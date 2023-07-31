#ifndef GRP_MACRO_SHADOWS_H
	#define GRP_MACRO_SHADOWS_H

	#include "Math.hlsl"
	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Shadow/ShadowSamplingTent.hlsl"

	#if defined(_DIRECTIONAL_PCF3)
		#define DIRECTIONAL_FILTER_SAMPLES 4
		#define DIRECTIONAL_FILTER_SETUP SampleShadow_ComputeSamples_Tent_3x3
	#elif defined(_DIRECTIONAL_PCF5)
		#define DIRECTIONAL_FILTER_SAMPLES 9
		#define DIRECTIONAL_FILTER_SETUP SampleShadow_ComputeSamples_Tent_5x5
	#elif defined(_DIRECTIONAL_PCF7)
		#define DIRECTIONAL_FILTER_SAMPLES 16
		#define DIRECTIONAL_FILTER_SETUP SampleShadow_ComputeSamples_Tent_7x7
	#endif

	#define MAX_SHADOWED_DIRECTIONAL_LIGHT_COUNT 4
	#define MAX_CASCADE_COUNT 4

	TEXTURE2D_SHADOW(_GRP_SHADOW_DIR_ATLAS);
	#define SHADOW_SAMPLER sampler_linear_clamp_compare
	SAMPLER_CMP(SHADOW_SAMPLER);

	CBUFFER_START(GRP_BUFFER_SHADOWS)
		int _CascadeCount;
		float4 _CascadeCullingSpheres[MAX_CASCADE_COUNT];
		float4 _CascadeData[MAX_CASCADE_COUNT];
		float4x4 _GRP_SHADOW_DIR_MATRICES[MAX_SHADOWED_DIRECTIONAL_LIGHT_COUNT * MAX_CASCADE_COUNT];
		float4 _GRP_SHADOW_DIR_ATLAS_SIZE;
		float4 _ShadowDistanceFade;
	CBUFFER_END

	float FadedShadowStrength(float distance, float scale, float fade) {
		return saturate((1.0 - distance * scale) * fade);
	}

	GRPShadowData GetShadowData(GRPSurface surfaceWS) {
		GRPShadowData data;
		data.cascadeBlend = 1.0;
		data.strength = FadedShadowStrength(
		surfaceWS.depth, _ShadowDistanceFade.x, _ShadowDistanceFade.y
		);
		int i;
		for (i = 0; i < _CascadeCount; i++) {
			float4 sphere = _CascadeCullingSpheres[i];
			float distanceSqr = DistanceSquared(surfaceWS.position, sphere.xyz);
			if (distanceSqr < sphere.w) {
				float fade = FadedShadowStrength(
				distanceSqr, _CascadeData[i].x, _ShadowDistanceFade.z
				);
				if (i == _CascadeCount - 1) {
					data.strength *= fade;
				}
				else {
					data.cascadeBlend = fade;
				}
				break;
			}
		}
		
		if (i == _CascadeCount) {
			data.strength = 0.0;
		}
		#if defined(_CASCADE_BLEND_DITHER)
			else if (data.cascadeBlend < surfaceWS.dither) {
				i += 1;
			}
		#endif
		#if !defined(_CASCADE_BLEND_SOFT)
			data.cascadeBlend = 1.0;
		#endif
		data.cascadeIndex = i;
		return data;
	}

	float SampleDirectionalShadowAtlas(float3 positionSTS) {
		return SAMPLE_TEXTURE2D_SHADOW(_GRP_SHADOW_DIR_ATLAS, SHADOW_SAMPLER, positionSTS);
	}

	float FilterDirectionalShadow(float3 positionSTS) {
		#if defined(DIRECTIONAL_FILTER_SETUP)
			float weights[DIRECTIONAL_FILTER_SAMPLES];
			float2 positions[DIRECTIONAL_FILTER_SAMPLES];
			float4 size = _GRP_SHADOW_DIR_ATLAS_SIZE.yyxx;
			DIRECTIONAL_FILTER_SETUP(size, positionSTS.xy, weights, positions);
			float shadow = 0;
			for (int i = 0; i < DIRECTIONAL_FILTER_SAMPLES; i++) {
				shadow += weights[i] * SampleDirectionalShadowAtlas(
				float3(positions[i].xy, positionSTS.z)
				);
			}
			return shadow;
		#else
			return SampleDirectionalShadowAtlas(positionSTS);
		#endif
	}

	float GetDirectionalShadowAttenuation (GRPDirectionalShadowData directional, GRPShadowData globalShadow, GRPSurface surfaceWS) {
		#if !defined(_RECEIVE_SHADOWS)
			return 1.0;
		#endif
		if (directional.strength <= 0.0) {
			return 1.0;
		}
		float3 normalBias = surfaceWS.normal *
		(directional.normalBias * _CascadeData[globalShadow.cascadeIndex].y);
		float3 positionSTS = mul(
		_GRP_SHADOW_DIR_MATRICES[directional.tileIndex],
		float4(surfaceWS.position + normalBias, 1.0)
		).xyz;
		float shadow = FilterDirectionalShadow(positionSTS);
		if (globalShadow.cascadeBlend < 1.0) {
			normalBias = surfaceWS.normal *
			(directional.normalBias * _CascadeData[globalShadow.cascadeIndex + 1].y);
			positionSTS = mul(
			_GRP_SHADOW_DIR_MATRICES[directional.tileIndex + 1],
			float4(surfaceWS.position + normalBias, 1.0)
			).xyz;
			shadow = lerp(
			FilterDirectionalShadow(positionSTS), shadow, globalShadow.cascadeBlend
			);
		}
		return lerp(1.0, shadow, directional.strength);
	}

#endif