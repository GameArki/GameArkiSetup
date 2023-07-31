#ifndef GRP_MACRO_LIGHTING_H
	#define GRP_MACRO_LIGHTING_H

	float3 IncomingLight(GRPSurface surface, GRPLight light) {
		return saturate(dot(surface.normal, light.direction) * light.attenuation) *	light.color;
	}

	float3 GetLighting(GRPSurface surface, GRPBRDF brdf, GRPLight light) {
		return IncomingLight(surface, light) * DirectBRDF(surface, brdf, light);
	}

	float3 GetLighting(GRPSurface surfaceWS, GRPBRDF brdf) {
		GRPShadowData shadowData = GetShadowData(surfaceWS);
		float3 color = 0.0;
		for (int i = 0; i < _GRP_LIGHT_DIR_COUNT; i++) {
			GRPLight light = GetDirectionalLight(i, surfaceWS, shadowData);
			color += GetLighting(surfaceWS, brdf, light);
		}
		return color;
	}

#endif