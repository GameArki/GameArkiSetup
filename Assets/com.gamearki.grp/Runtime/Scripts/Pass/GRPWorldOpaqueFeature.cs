using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPWorldOpaqueFeature {

        public GRPWorldOpaqueFeature() {}

        public void Draw(GRPSettingsModel settings, ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            SortingSettings sortingSettings = new SortingSettings(camera);
            sortingSettings.criteria = SortingCriteria.CommonOpaque;

            DrawingSettings drawingSettings = new DrawingSettings();
            drawingSettings.SetShaderPassName(0, new ShaderTagId(settings.lightMode_opaque));
            drawingSettings.sortingSettings = sortingSettings;
            drawingSettings.enableDynamicBatching = settings.batching_useDynamic;
            drawingSettings.enableInstancing = settings.batching_useGPUInstancing;

            FilteringSettings filteringSettings = FilteringSettings.defaultValue;
            filteringSettings.renderQueueRange = RenderQueueRange.opaque;

            ctx.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        }

    }
}