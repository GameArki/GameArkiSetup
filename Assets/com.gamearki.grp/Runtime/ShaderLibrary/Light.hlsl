#ifndef GRP_MACRO_LIGHT_H
	#define GRP_MACRO_LIGHT_H

	#define GRP_MAX_DIRECTIONAL_LIGHT_COUNT 4

	CBUFFER_START(GRP_BUFFER_LIGHT)
		int _GRP_LIGHT_DIR_COUNT;
		float4 _GRP_LIGHT_DIR_COLORS[GRP_MAX_DIRECTIONAL_LIGHT_COUNT];
		float4 _GRP_LIGHT_DIR_DIRS[GRP_MAX_DIRECTIONAL_LIGHT_COUNT];
		float4 _GRP_LIGHT_DIR_SHADOW_DATAS[GRP_MAX_DIRECTIONAL_LIGHT_COUNT];
	CBUFFER_END

	GRPDirectionalShadowData GetDirectionalShadowData(int lightIndex, GRPShadowData shadowData) {
		GRPDirectionalShadowData data;
		data.strength =	_GRP_LIGHT_DIR_SHADOW_DATAS[lightIndex].x * shadowData.strength;
		data.tileIndex = _GRP_LIGHT_DIR_SHADOW_DATAS[lightIndex].y + shadowData.cascadeIndex;
		data.normalBias = _GRP_LIGHT_DIR_SHADOW_DATAS[lightIndex].z;
		return data;
	}

	GRPLight GetDirectionalLight(int index, GRPSurface surfaceWS, GRPShadowData shadowData) {
		GRPLight light;
		light.color = _GRP_LIGHT_DIR_COLORS[index].rgb;
		light.direction = _GRP_LIGHT_DIR_DIRS[index].xyz;
		GRPDirectionalShadowData dirShadowData = GetDirectionalShadowData(index, shadowData);
		light.attenuation = GetDirectionalShadowAttenuation(dirShadowData, shadowData, surfaceWS);
		return light;
	}

#endif