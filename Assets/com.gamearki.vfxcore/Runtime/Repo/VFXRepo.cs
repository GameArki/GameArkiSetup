using System;
using System.Collections.Generic;

namespace GameArki.VFX {

    public class VFXRepo {

        // Occupied VFX Dic
        Dictionary<int, VFXPlayerEntity> playingDic;

        // Idle VFX Dic
        Dictionary<string, List<VFXPlayerEntity>> idleDic;

        public VFXRepo() {
            this.playingDic = new Dictionary<int, VFXPlayerEntity>();
            this.idleDic = new Dictionary<string, List<VFXPlayerEntity>>();
        }

        public void AddToPlaying(VFXPlayerEntity entity) {
            playingDic.Add(entity.VFXID, entity);
        }

        public void AddToIdle(VFXPlayerEntity entity) {
            var vfxName = entity.Name;
            if (!idleDic.ContainsKey(vfxName)) {
                idleDic.Add(vfxName, new List<VFXPlayerEntity>());
            }
            idleDic[vfxName].Add(entity);
        }

        public void RemoveFromPlaying(int id) {
            playingDic.Remove(id);
        }

        public bool TryFetchFromIdle(string vfxName, out VFXPlayerEntity entity) {
            entity = null;
            if (!idleDic.ContainsKey(vfxName)) {
                return false;
            }

            var list = idleDic[vfxName];
            var count = list.Count;
            if (count == 0) {
                return false;
            }

            var removeIndex = count - 1;
            entity = list[removeIndex];
            list.RemoveAt(removeIndex);
            return true;
        }

        public bool TryGetByID(int id, out VFXPlayerEntity entity) {
            return playingDic.TryGetValue(id, out entity);
        }

        public void Foreach(Action<int, VFXPlayerEntity> action) {
            var e = playingDic.GetEnumerator();
            while (e.MoveNext()) {
                var kvp = e.Current;
                action(kvp.Key, kvp.Value);
            }
        }

    }

}