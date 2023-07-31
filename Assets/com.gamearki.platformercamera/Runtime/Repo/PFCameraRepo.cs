using System.Collections.Generic;
using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Repo {

    internal class PFCameraRepo {

        Dictionary<int, PFCameraEntity> all;

        PFCameraEntity current;
        public PFCameraEntity Current => current;

        internal PFCameraRepo() {
            this.all = new Dictionary<int, PFCameraEntity>();
        }

        internal void Add(PFCameraEntity value) {
            bool has = all.TryAdd(value.ID, value);
            if (has) {
                if (current == null) {
                    current = value;
                }
            }
        }

        internal void SetCurrent(PFCameraEntity value) {
            this.current = value;
        }

    }

}