#ifndef GRP_MACRO_DATATYPE_H
#define GRP_MACRO_DATATYPE_H

struct GRPLight {
	float3 color;
	float3 direction;
	float attenuation;
};

struct GRPBRDF {
	float3 diffuse;
	float3 specular;
	float roughness;
};

struct GRPShadowData {
	int cascadeIndex;
	float cascadeBlend;
	float strength;
};

struct GRPDirectionalShadowData {
	float strength;
	int tileIndex;
	float normalBias;
};

struct GRPSurface {
	float3 position;
	float3 normal;
	float3 viewDirection;
	float depth;
	float3 color;
	float alpha;
	float metallic;
	float smoothness;
	float dither;
};

#endif