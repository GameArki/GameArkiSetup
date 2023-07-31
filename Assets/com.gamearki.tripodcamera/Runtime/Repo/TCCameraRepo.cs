using System;
using System.Collections.Generic;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera {

    public class TCCameraRepo {

        TCCameraEntity currentTCCam;
        public TCCameraEntity CurrentTCCam => currentTCCam;
        public void SetCurrentTCCam(TCCameraEntity value) => currentTCCam = value;

        Dictionary<int, TCCameraEntity> all;
        public int Count => all.Count;

        public TCCameraRepo() {
            this.all = new Dictionary<int, TCCameraEntity>();
        }

        public bool TryAdd(TCCameraEntity camera) {
            return all.TryAdd(camera.ID, camera);
        }

        public bool TryGet(int id, out TCCameraEntity entity) {
            return all.TryGetValue(id, out entity);
        }

        public void Remove(int id) {
            all.Remove(id);
            if (all.Count == 0) {
                currentTCCam = null;
            }
        }

        public void Clear() {
            all.Clear();
            currentTCCam = null;
        }

        public void ForeachAll(Action<TCCameraEntity> action) {
            foreach (var pair in all) {
                action(pair.Value);
            }
        }

    }

}