using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Template;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCCameraAPI : ITCCameraAPI {

        TCCameraDomain cameraDomain;

        public TCCameraAPI() { }

        public void Inject(TCCameraDomain cameraDomain) {
            this.cameraDomain = cameraDomain;
        }

        void ITCCameraAPI.ApplyTM(in TCCameraTM tm, int id) {
            cameraDomain.ApplyTM(tm, id);
        }

        void ITCCameraAPI.Remove(int id) {
            cameraDomain.Remove(id);
        }

        bool ITCCameraAPI.SetActive(bool active, int id) {
            return cameraDomain.SetActive(active, id);
        }

        void ITCCameraAPI.SetPosition(in Vector3 pos, int id) {
            cameraDomain.SetPosition(pos, id);
        }

        void ITCCameraAPI.SetRotation(in Quaternion rot, int id) {
            cameraDomain.SetRotation(rot, id);
        }

        int ITCCameraAPI.Spawn(in Vector3 position, in Quaternion rotation, float fov) {
            return cameraDomain.Spawn(position, rotation, fov);
        }
  
    }

}