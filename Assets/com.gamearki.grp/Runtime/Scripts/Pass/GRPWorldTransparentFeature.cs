using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPWorldTransparentFeature {

        public GRPWorldTransparentFeature() { }

        public void Draw(GRPSettingsModel settings, ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            SortingSettings sortingSettings = new SortingSettings(camera);
            sortingSettings.criteria = SortingCriteria.CommonTransparent;

            DrawingSettings drawingSettings = new DrawingSettings();
            drawingSettings.SetShaderPassName(0, new ShaderTagId(settings.lightMode_transparent));
            drawingSettings.sortingSettings = sortingSettings;
            // drawingSettings.enableDynamicBatching = settings.batching_UseDynamic;
            // drawingSettings.enableInstancing = settings.batching_UseGPUInstancing;

            FilteringSettings filteringSettings = FilteringSettings.defaultValue;
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;

            ctx.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        }

    }
}