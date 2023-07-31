using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPWorldPostProcessingFeature {

        public GRPWorldPostProcessingFeature() {}

        public void Draw(GRPSettingsModel settings, ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            SortingSettings sortingSettings = new SortingSettings(camera);
            sortingSettings.criteria = SortingCriteria.CommonOpaque;

            DrawingSettings drawingSettings = new DrawingSettings();
            drawingSettings.SetShaderPassName(0, new ShaderTagId(settings.lightMode_worldPP));
            drawingSettings.sortingSettings = sortingSettings;
            // drawingSettings.enableDynamicBatching = settings.batching_UseDynamic;
            // drawingSettings.enableInstancing = settings.batching_UseGPUInstancing;

            FilteringSettings filteringSettings = FilteringSettings.defaultValue;
            filteringSettings.renderQueueRange = RenderQueueRange.all;

            ctx.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        }
    }
}