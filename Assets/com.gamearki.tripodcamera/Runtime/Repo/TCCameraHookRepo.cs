using System.Collections.Generic;
using GameArki.TripodCamera.Hook;

namespace GameArki.TripodCamera {

    public class TCCameraHookRepo {

        List<TCCameraHook> all;

        public TCCameraHookRepo() {
            this.all = new List<TCCameraHook>();
        }

        public void Add(TCCameraHook hook) {
            all.Add(hook);
        }

        public void Remove(TCCameraHook hook) {
            all.Remove(hook);
        }

        public IEnumerable<TCCameraHook> GetAll() {
            return all;
        }

    }

}