using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPMain : RenderPipeline {

        GRPSettingsModel settings;

        GRPCameraFeature cameraFeature;
        GRPLightFeature lightFeature;
        GRPWorldOpaqueFeature worldOpaqueFeature;
        GRPWorldTransparentFeature worldTransparentFeature;
        GRPWorldUIFeature worldUIFeature;
        GRPWorldPostProcessingFeature worldPostProcessingFeature;
        GRPShadowFeature shadowFeature;

        public GRPMain(GRPSettingsModel settings) {
            this.cameraFeature = new GRPCameraFeature();
            this.lightFeature = new GRPLightFeature(settings);
            this.worldOpaqueFeature = new GRPWorldOpaqueFeature();
            this.worldTransparentFeature = new GRPWorldTransparentFeature();
            this.worldUIFeature = new GRPWorldUIFeature();
            this.worldPostProcessingFeature = new GRPWorldPostProcessingFeature();
            this.shadowFeature = new GRPShadowFeature();

            this.settings = settings;

            GraphicsSettings.useScriptableRenderPipelineBatching = settings.batching_useSRPBatcher;
            GraphicsSettings.lightsUseLinearIntensity = settings.light_useLinearIntensity;

        }

        protected override void Render(ScriptableRenderContext ctx, Camera[] cameras) {

            for (int i = 0; i < cameras.Length; i += 1) {
                var camera = cameras[i];

                bool isNeedDraw = camera.TryGetCullingParameters(out ScriptableCullingParameters cullingParameters);
                if (!isNeedDraw) {
                    continue;
                }

                cullingParameters.shadowDistance = Mathf.Min(settings.shadow_maxDistance, camera.farClipPlane);
                CullingResults cullingResults = ctx.Cull(ref cullingParameters);

                // [[ Camera Sample
                cameraFeature.BeginSample();
                cameraFeature.Execute(ctx);

                // [[[[ Light Sample
                lightFeature.BeginSample();

                // Lights: Setup
                lightFeature.Setup(settings, ctx, camera, ref cullingResults);

                // Shadows: Bake ShadowMap && Draw
                shadowFeature.BakeShadowRT(settings, lightFeature.Shadow_DirLightIndices, ctx);

                // [[[[[[ Shadow Sample
                shadowFeature.BeginSample();
                shadowFeature.Execute(ctx);
                shadowFeature.Draw(settings, lightFeature.Shadow_DirLightIndices, ctx, camera, ref cullingResults);
                shadowFeature.Setup();

                // ]]]]]] Shadow Sample
                shadowFeature.EndSample();

                // Shadows: Execute
                shadowFeature.Execute(ctx);

                // ]]]] Light Sample
                lightFeature.EndSample();

                // Lights: Execute
                lightFeature.Execute(ctx);

                // ]] Camera Sample
                cameraFeature.EndSample();

                // Camera: Setup
                cameraFeature.Setup(ctx, camera);

                // Camera: Draw SkyBox
                cameraFeature.DrawSkybox(ctx, camera, ref cullingResults);

                // [[[[ Camera Sample
                cameraFeature.BeginSample();

                // Camera: Execute
                cameraFeature.Execute(ctx);

                // World Opaque: Draw
                worldOpaqueFeature.Draw(settings, ctx, camera, ref cullingResults);

                // World Transparent: Draw
                worldTransparentFeature.Draw(settings, ctx, camera, ref cullingResults);

                // World PostProcessing: Draw
                worldPostProcessingFeature.Draw(settings, ctx, camera, ref cullingResults);

                // World UI: Draw
                worldUIFeature.Draw(settings, ctx, camera, ref cullingResults);

                // Shadows: Cleanup
                shadowFeature.Cleanup(ctx);

                // ]]]] Camera Sample
                cameraFeature.EndSample();

                // Camera: Execute
                cameraFeature.Execute(ctx);

                // Submit
                ctx.Submit();

            }

        }

    }

}
