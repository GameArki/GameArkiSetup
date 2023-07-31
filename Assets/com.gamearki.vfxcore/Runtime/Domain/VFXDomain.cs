using UnityEngine;

namespace GameArki.VFX {

    public class VFXDomain {

        VFXContext context;

        public VFXDomain() {

        }

        public void Inject(VFXContext context) {
            this.context = context;
        }

        public int TryPlayVFX_Default(string vfxName, Transform parent = null) {
            if (vfxName == null) {
                return -1;
            }

            if (!TryGetOrSpawnVFXPlayer(vfxName, out var player)) {
                return -1;
            }

            player.DefaultPlay();
            player.SetParent(parent);
            player.SetManualTick(false);

            var repo = context.Repo;
            repo.AddToPlaying(player);

            return player.VFXID;
        }

        public int TryPlayVFX(string vfxName, bool isLoop, Transform parent = null) {
            if (vfxName == null) {
                return -1;
            }

            if (!TryGetOrSpawnVFXPlayer(vfxName, out var player)) {
                return -1;
            }

            player.Play(isLoop);
            player.SetParent(parent);
            player.SetManualTick(false);

            var repo = context.Repo;
            repo.AddToPlaying(player);

            return player.VFXID;
        }

        bool TryGetOrSpawnVFXPlayer(string vfxName, out VFXPlayerEntity player) {
            player = null;

            var repo = context.Repo;
            if (!repo.TryFetchFromIdle(vfxName, out player)) {
                // Create a new vfx player
                player = VFXFactory.CreateVFXPlayerEntity(vfxName);
                var vfxAssetDic = context.VFXAssetDic;
                if (!vfxAssetDic.TryGetValue(vfxName, out var vfxPrefab)) {
                    return false;
                }

                player.SetName(vfxName);
                player.SetVFXGo(GameObject.Instantiate(vfxPrefab));
            }

            return true;
        }

        public int TryPlayVFX_ManualTick(string vfxName, int tickCount, Transform parent = null) {
            var repo = context.Repo;

            // Try to get an idle vfx player
            if (!repo.TryFetchFromIdle(vfxName, out var player)) {
                // Create a new vfx player
                player = VFXFactory.CreateVFXPlayerEntity(vfxName);
                var vfxAssetDic = context.VFXAssetDic;
                if (!vfxAssetDic.TryGetValue(vfxName, out var vfxPrefab)) {
                    return -1;
                }

                player.SetName(vfxName);
                player.SetVFXGo(GameObject.Instantiate(vfxPrefab));
            }

            player.Play(true);
            player.SetManualTick(true);
            player.SetTickCount(tickCount);
            player.SetParent(parent);
            repo.AddToPlaying(player);

            return player.VFXID;
        }

        public bool TryStopVFX(int vfxID) {
            var repo = context.Repo;
            if (!repo.TryGetByID(vfxID, out var player)) {
                return false;
            }

            player.StopAll();
            player.SetParent(context.VFXRoot.transform);
            return true;
        }

    }

}