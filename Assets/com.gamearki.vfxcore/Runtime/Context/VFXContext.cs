using System.Collections.Generic;
using UnityEngine;

namespace GameArki.VFX {

    public class VFXContext {

        // ==== External Dependencies ====
        public Transform VFXRoot { get; private set; }

        // ==== Repos ====
        VFXRepo repo;
        public VFXRepo Repo => repo;

        public Dictionary<string, GameObject> VFXAssetDic { get; private set; }

        public VFXContext() {
            repo = new VFXRepo();
            VFXAssetDic = new Dictionary<string, GameObject>();
        }

        public void Inject(GameObject[] vfxAssets, Transform vfxRoot) {
            this.VFXRoot = vfxRoot;

            var len = vfxAssets.Length;
            for (int i = 0; i < len; i++) {
                var vfxAsset = vfxAssets[i];
                if (!VFXAssetDic.TryAdd(vfxAsset.name, vfxAsset)) {
                    Debug.LogWarning($"VFXCore: VFXAsset {vfxAsset.name} already exists in the dictionary.");
                }
            }
        }

    }

}