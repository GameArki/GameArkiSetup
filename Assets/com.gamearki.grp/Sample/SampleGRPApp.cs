using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GRP.Sample {

    public class SampleGRPApp : MonoBehaviour {

        void Awake() {
            Application.targetFrameRate = 120;

            Action action = async () => {
                var rp = QualitySettings.renderPipeline;
                int idx = 20;
                while (rp == null && idx > 0) {
                    rp = QualitySettings.GetRenderPipelineAssetAt(0);
                    await Task.Delay(10);
                    idx -= 1;
                }
            };
            action.Invoke();
        }

        void Start() {
        }

        void OnEnable() {
        }

        void OnValidate() {
        }

    }

}
