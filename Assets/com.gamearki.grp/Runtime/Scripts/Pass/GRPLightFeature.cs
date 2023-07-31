using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPLightFeature {

        const string CMD_NAME = GRPNameCollection.CMD_LIGHT;
        CommandBuffer cmd;

        // ==== TEMP ====
        Vector4[] light_dir_positions;
        Vector4[] light_dir_colors;
        Vector4[] light_point_positions;
        Vector4[] light_point_colors;
        Vector4[] light_spot_positions;
        Vector4[] light_spot_colors;

        List<int> shadow_dir_light_indices;
        public List<int> Shadow_DirLightIndices => shadow_dir_light_indices;

        public GRPLightFeature(GRPSettingsModel settings) {

            this.light_dir_colors = new Vector4[settings.shadow_dir_maxLightCount];
            this.light_dir_positions = new Vector4[settings.shadow_dir_maxLightCount];

            this.light_point_colors = new Vector4[settings.light_maxPointLight];
            this.light_point_positions = new Vector4[settings.light_maxPointLight];

            this.light_spot_colors = new Vector4[settings.light_maxSpotLight];
            this.light_spot_positions = new Vector4[settings.light_maxSpotLight];

            this.shadow_dir_light_indices = new List<int>();

            cmd = CommandBufferPool.Get(CMD_NAME);

        }

        public void BeginSample() {
            cmd.BeginSample(CMD_NAME);
        }

        public void EndSample() {
            cmd.EndSample(CMD_NAME);
        }

        public void Execute(ScriptableRenderContext ctx) {
            ctx.ExecuteCommandBuffer(cmd);
            cmd.Clear();
        }

        public void Setup(GRPSettingsModel settings, ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            // Light
            var visibleLights = cullingResults.visibleLights;

            shadow_dir_light_indices.Clear();

            int directionalLightsCount = 0;
            int pointLightsCount = 0;
            int spotLightsCount = 0;

            int dirShadowCount = 0;

            for (int i = 0; i < visibleLights.Length; i += 1) {
                var light = visibleLights[i];
                if (light.lightType == LightType.Directional) {
                    if (directionalLightsCount >= settings.light_maxDirLight) {
                        continue;
                    }
                    light_dir_colors[i] = light.finalColor;
                    light_dir_positions[i] = -light.localToWorldMatrix.GetColumn(2);

                    bool allowShadow = dirShadowCount < settings.shadow_dir_maxLightCount;
                    allowShadow &= light.light.shadows != LightShadows.None;
                    allowShadow &= light.light.shadowStrength > 0;
                    allowShadow &= cullingResults.GetShadowCasterBounds(directionalLightsCount, out Bounds shadowBounds);
                    if (allowShadow) {
                        dirShadowCount += 1;
                        shadow_dir_light_indices.Add(i);
                    }

                    directionalLightsCount += 1;

                } else if (light.lightType == LightType.Point) {
                    if (pointLightsCount >= settings.light_maxPointLight) {
                        continue;
                    }
                    light_point_colors[i]= light.finalColor;
                    light_point_positions[i]= -light.localToWorldMatrix.GetColumn(2);
                    pointLightsCount += 1;
                } else if (light.lightType == LightType.Spot) {
                    if (spotLightsCount >= settings.light_maxSpotLight) {
                        continue;
                    }
                    light_spot_colors[i]= light.finalColor;
                    light_spot_positions[i]= -light.localToWorldMatrix.GetColumn(2);
                    spotLightsCount += 1;
                }
            }

            cmd.SetGlobalInt(GRPNameCollection.LIGHT_DIR_COUNT, directionalLightsCount);
            if (directionalLightsCount > 0) {
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_DIR_DIRS, light_dir_positions);
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_DIR_COLORS, light_dir_colors);
            }

            cmd.SetGlobalInt(GRPNameCollection.LIGHT_POINT_COUNT, pointLightsCount);
            if (pointLightsCount > 0) {
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_POINT_POSES, light_point_positions);
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_POINT_COLORS, light_point_colors);
            }

            cmd.SetGlobalInt(GRPNameCollection.LIGHT_SPOT_COUNT, spotLightsCount);
            if (spotLightsCount > 0) {
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_SPOT_POSES, light_spot_positions);
                cmd.SetGlobalVectorArray(GRPNameCollection.LIGHT_SPOT_COLORS, light_spot_colors);
            }

        }

    }
}