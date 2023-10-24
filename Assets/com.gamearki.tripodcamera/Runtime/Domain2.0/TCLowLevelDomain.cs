using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;
using UnityEngine;

namespace GameArki.TripodCamera.Domain {

    public class TCLowLevelDomain {

        TCContext context;

        public TCLowLevelDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public void Push(float value, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var infoCom = tcCam.BeforeInfo;
            var mainCam = context.MainCamera;
            var forward = mainCam.transform.forward;
            infoCom.SetPosition(infoCom.Position + forward * value);
        }

        public void Move(in Vector2 value, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Move(value);
        }

        public void Move_AndChangeLookAtOffset(in Vector2 value, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Move_AndChangeLookAtOffset(value);
        }

        public void Rotate_Roll(float degree, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var curInfoCom = tcCam.BeforeInfo;
            var euler = curInfoCom.Rotation.eulerAngles;
            euler.z += degree;
            curInfoCom.SetRotation(Quaternion.Euler(euler));
        }

        public void Zoom(float addition, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var infoCom = tcCam.BeforeInfo;
            infoCom.AddFOV(addition);
        }

        bool _TryGetTCCameraByID(int id, out TCCameraEntity tcCam) {
            var repo = context.CameraRepo;
            if (id == -1) {
                tcCam = repo.CurrentTCCam;
            } else {
                repo.TryGet(id, out tcCam);
            }

            return tcCam != null;
        }

    }

}