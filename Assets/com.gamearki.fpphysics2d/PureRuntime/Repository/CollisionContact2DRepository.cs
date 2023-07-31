using System.Linq;
using System.Collections.Generic;

namespace GameArki.FPPhysics2D {

    public class CollisionContact2DRepository {

        Dictionary<ulong, CollisionContact2DModel> all;

        public CollisionContact2DRepository() {
            this.all = new Dictionary<ulong, CollisionContact2DModel>();
        }

        public void Add(CollisionContact2DModel model) {
            all.Add(model.key, model);
        }

        public bool Contains(ulong key) {
            return all.ContainsKey(key);
        }

        public bool TryGet(ulong key, out CollisionContact2DModel model) {
            return all.TryGetValue(key, out model);
        }

        public KeyValuePair<ulong, CollisionContact2DModel>[] GetAll() {
            return all.ToArray();
        }

        public bool Remove(ulong key) {
            return all.Remove(key);
        }

        public void Clear() {
            all.Clear();
        }

    }

}