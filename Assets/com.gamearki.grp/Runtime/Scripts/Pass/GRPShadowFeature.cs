using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPShadowFeature {

        const string CMD_NAME = GRPNameCollection.CMD_SHADOW;
        CommandBuffer cmd;

        const int maxShadowedDirLightCount = 4;
        const int maxCascades = 4;
        Matrix4x4[] dirShadowMatrices = new Matrix4x4[maxShadowedDirLightCount * maxCascades];
        int atlasSize;

        public GRPShadowFeature() {
            cmd = CommandBufferPool.Get(CMD_NAME);
            atlasSize = 1;
        }

        public void BeginSample() {
            cmd.BeginSample(CMD_NAME);
        }

        public void EndSample() {
            cmd.EndSample(CMD_NAME);
        }

        public void Execute(ScriptableRenderContext ctx) {
            M_Execute(ctx);
        }

        void M_Execute(ScriptableRenderContext ctx) {
            ctx.ExecuteCommandBuffer(cmd);
            cmd.Clear();
        }

        public void BakeShadowRT(GRPSettingsModel settings, List<int> dirLightIndices, ScriptableRenderContext ctx) {
            if (dirLightIndices.Count > 0) {
                atlasSize = (int)settings.shadow_dir_shadowMapSize;
            }
            cmd.GetTemporaryRT(GRPNameCollection.ID_SHADOW_DIR_ATLAS,
                               atlasSize,
                               atlasSize,
                               settings.shadow_depth_bit,
                               FilterMode.Bilinear,
                               RenderTextureFormat.Shadowmap);
            cmd.SetRenderTarget(GRPNameCollection.ID_SHADOW_DIR_ATLAS, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
            cmd.ClearRenderTarget(true, false, Color.clear);
        }

        public void Draw(GRPSettingsModel settings, List<int> dirLightIndices, ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            int split = dirLightIndices.Count <= 1 ? 1 : 2;
            int tileSize = atlasSize / split;
            int matricesCount = dirLightIndices.Count;
            Matrix4x4[] matrices = new Matrix4x4[matricesCount];
            for (int i = 0; i < dirLightIndices.Count; i += 1) {
                int visibleLightIndex = dirLightIndices[i];
                var shadowDrawingSettings = new ShadowDrawingSettings(cullingResults, visibleLightIndex);
                cullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(visibleLightIndex,
                                                                                    0,
                                                                                    1,
                                                                                    Vector3.zero,
                                                                                    tileSize,
                                                                                    0f,
                                                                                    out Matrix4x4 viewMatrix,
                                                                                    out Matrix4x4 projectionMatrix,
                                                                                    out ShadowSplitData splitData);
                if (SystemInfo.usesReversedZBuffer) {
                    projectionMatrix.m20 = -projectionMatrix.m20;
                    projectionMatrix.m21 = -projectionMatrix.m21;
                    projectionMatrix.m22 = -projectionMatrix.m22;
                    projectionMatrix.m23 = -projectionMatrix.m23;
                }
                var scaleOffset = Matrix4x4.TRS(Vector3.one * 0.5f, Quaternion.identity, Vector3.one * 0.5f);
                shadowDrawingSettings.splitData = splitData;
                cmd.SetViewProjectionMatrices(viewMatrix, projectionMatrix);
                // matrices[i] = scaleOffset * (projectionMatrix * viewMatrix);
                cmd.SetGlobalMatrix(GRPNameCollection.ID_SHADOW_DIR_MATRICES, scaleOffset * (projectionMatrix * viewMatrix));
                M_Execute(ctx);
                ctx.DrawShadows(ref shadowDrawingSettings);
            }

        }

        public void Setup() {
            cmd.SetGlobalTexture(GRPNameCollection.ID_SHADOW_DIR_ATLAS, GRPNameCollection.ID_SHADOW_DIR_ATLAS);
        }

        public void Cleanup(ScriptableRenderContext ctx) {
            cmd.ReleaseTemporaryRT(GRPNameCollection.ID_SHADOW_DIR_ATLAS);
            M_Execute(ctx);
        }

    }

}