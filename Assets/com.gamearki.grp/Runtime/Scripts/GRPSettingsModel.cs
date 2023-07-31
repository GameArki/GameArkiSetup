using System;
using UnityEngine;

namespace GRP {

    [Serializable]
    public class GRPSettingsModel {

        // Light Mode
        [Header("Light Mode")]
        public string lightMode_opaque = "GRP_World_Opaque";
        public string lightMode_transparent = "GRP_World_Transparent";
        public string lightMode_worldPP = "GRP_World_PP";
        public string lightMode_worldUI = "GRP_World_UI";

        // Batching
        [Header("Batching")]
        public bool batching_useDynamic = true;
        public bool batching_useGPUInstancing = true;
        public bool batching_useSRPBatcher = true;

        // Light
        [Header("Light")]
        public int light_maxDirLight = 1;
        public int light_maxPointLight = 4;
        public int light_maxSpotLight = 4;
        public bool light_useLinearIntensity = true;

        // Shadow
        [Header("Shadow")]
        [Min(0)] public float shadow_maxDistance = 100f;
        public byte shadow_depth_bit = 32;
        public int shadow_dir_maxLightCount = 1;
        public GRPTextureSize shadow_dir_shadowMapSize = GRPTextureSize._1024;
        public int shadow_dir_cascadeCount = 4;
        public float shadow_dir_cascadeSplit0 = 0.05f;
        public float shadow_dir_cascadeSplit1 = 0.15f;
        public float shadow_dir_cascadeSplit2 = 0.3f;
        public float shadow_dir_cascadeSplit3 = 0.5f;
        public float shadow_dir_bias = 0.005f;
        public float shadow_dir_normalBias = 0.4f;
        public float shadow_dir_fade = 0.5f;

    }

}