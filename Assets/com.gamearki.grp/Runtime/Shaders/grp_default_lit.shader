Shader "GRP/grp_default_lit" {

    Properties {
        _BaseMap ("Base (RGB)", 2D) = "white" {}
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }

    SubShader {

        pass{

            Tags { "RenderType"="Opaque" "LightMode"="GRP_World_Opaque" }
            LOD 100

            HLSLPROGRAM
            #pragma target 3.5
			#pragma shader_feature _CLIPPING
			#pragma shader_feature _RECEIVE_SHADOWS
			#pragma shader_feature _PREMULTIPLY_ALPHA
			#pragma multi_compile _ _DIRECTIONAL_PCF3 _DIRECTIONAL_PCF5 _DIRECTIONAL_PCF7
			#pragma multi_compile _ _CASCADE_BLEND_SOFT _CASCADE_BLEND_DITHER
			#pragma multi_compile_instancing
			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment

            #include "../ShaderLibrary/Pass/LitPass.hlsl"
 
            ENDHLSL
        }

        Pass {
			Tags {
				"LightMode" = "ShadowCaster"
			}

			ColorMask 0

			HLSLPROGRAM
			#pragma target 3.5
			#pragma shader_feature _ _SHADOWS_CLIP _SHADOWS_DITHER
			#pragma multi_compile_instancing
			#pragma vertex ShadowCasterPassVertex
			#pragma fragment ShadowCasterPassFragment
			#include "../ShaderLibrary/Pass/ShadowCasterPass.hlsl"
			ENDHLSL
		}

    }

}