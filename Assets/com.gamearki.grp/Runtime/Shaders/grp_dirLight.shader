Shader "GRP/grp_dirLight"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode"="GRP_World_Opaque" }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4x4 unity_ObjectToWorld;
            float4x4 unity_MatrixVP;

            struct appdata {
                float4 opos : POSITION;
            };

            struct v2f {
                float4 cpos : SV_POSITION;
                float4 wpos : TEXCOORD0;
            };

            float4 _Color;
            float4 _GRP_CAMERA_DIR;

            v2f vert (appdata v) {
                v2f o;
                o.wpos = mul(unity_ObjectToWorld, v.opos);
                o.cpos = mul(unity_MatrixVP, o.wpos);
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 p = normalize(_GRP_CAMERA_DIR.xy);
                return float4(p.xy, 1, 1);
            }
            ENDHLSL
        }
    }
}
