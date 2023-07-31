using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    public class GRPCameraFeature {

        const string CMD_NAME = GRPNameCollection.CMD_CAMERA;
        CommandBuffer cmd;

        public GRPCameraFeature() {
            cmd = CommandBufferPool.Get(CMD_NAME);
        }

        public void Setup(ScriptableRenderContext ctx, Camera camera) {
            ctx.SetupCameraProperties(camera);
            cmd.SetGlobalVector(Shader.PropertyToID(GRPNameCollection.CAMERA_POS), camera.transform.position);
            cmd.SetGlobalVector(Shader.PropertyToID(GRPNameCollection.CAMERA_DIR), camera.transform.forward);
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

        public void DrawSkybox(ScriptableRenderContext ctx, Camera camera, ref CullingResults cullingResults) {

            var flag = camera.clearFlags;
            // Skybox || Solid Color
            if (flag == CameraClearFlags.Skybox) {
                ctx.DrawSkybox(camera);
            } else if (flag == CameraClearFlags.SolidColor || flag == CameraClearFlags.Color) {
                cmd.ClearRenderTarget(true, true, camera.backgroundColor);
            }

        }

    }

}