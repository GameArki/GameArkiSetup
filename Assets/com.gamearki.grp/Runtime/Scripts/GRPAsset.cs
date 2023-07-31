using UnityEngine;
using UnityEngine.Rendering;

namespace GRP {

    [CreateAssetMenu(menuName = "GRP/CreateAsset")]
    public class GRPAsset : RenderPipelineAsset {

        [SerializeField] GRPSettingsModel settings;

        protected override RenderPipeline CreatePipeline() {
            var grp = new GRPMain(settings);
            return grp;
        }

    }
}