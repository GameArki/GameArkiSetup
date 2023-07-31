using System.Collections.Generic;
using UnityEngine;

namespace GameArki.VFX {

    public class VFXCore {

        VFXCoreAPI api;
        public IVFXCoreAPI API => api;

        VFXContext vfxContext;
        VFXDomain vfxDomain;

        List<int> vfxPlayerRemoveList;

        public VFXCore() {
            api = new VFXCoreAPI();
            vfxContext = new VFXContext();
            vfxDomain = new VFXDomain();
            vfxPlayerRemoveList = new List<int>();
        }

        public void Tick() {
            vfxPlayerRemoveList.Clear();
            var vfxRepo = vfxContext.Repo;
            vfxRepo.Foreach((key, vfxPlayerEntity) => {
                if (vfxPlayerEntity.IsManualTick) {
                    vfxPlayerEntity.ReduceTickCount();
                    if (vfxPlayerEntity.TickCount <= 0) {
                        vfxPlayerEntity.StopAll();
                        vfxRepo.AddToIdle(vfxPlayerEntity);
                        vfxPlayerRemoveList.Add(vfxPlayerEntity.VFXID);
                    }
                    return;
                }

                if (vfxPlayerEntity.IsAllStopped()) {
                    vfxRepo.AddToIdle(vfxPlayerEntity);
                    vfxPlayerRemoveList.Add(vfxPlayerEntity.VFXID);
                }
                return;
            });

            vfxPlayerRemoveList.ForEach((key) => {
                vfxRepo.RemoveFromPlaying(key);
            });
        }

        public void Inject(GameObject[] vfxAssets) {
            var vfxRoot = new GameObject("VFXCore_Pool");
            GameObject.DontDestroyOnLoad(vfxRoot);

            vfxContext.Inject(vfxAssets, vfxRoot.transform);

            vfxDomain.Inject(vfxContext);
            api.Inject(vfxContext, vfxDomain);
        }

    }

}