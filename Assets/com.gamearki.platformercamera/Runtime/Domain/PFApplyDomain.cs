using UnityEngine;
using GameArki.PlatformerCamera.Facades;
using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Domain {

    internal class PFApplyDomain {

        AllPFContext ctx;

        internal PFApplyDomain() {}

        internal void Inject(AllPFContext ctx) {
            this.ctx = ctx;
        }

        internal void Apply(PFCameraEntity pfCam, Camera mainCam, float dt) {

            var infoCom = pfCam.CurrentInfoCom;
            var followCom = pfCam.FollowCom;
            var ShakeCom = pfCam.ShakeCom;

            Vector3 pos;
            if (followCom.HasTarget()) {
                pos = followCom.GetFollowPos();
                infoCom.SetPos(pos);
            } else {
                pos = infoCom.Pos;
            }

            // Apply Confiner
            var confiner = pfCam.ConfinerCom;
            if (confiner.IsEnable) {
                var camPos = confiner.LockCameraInside(pos, pfCam.CurrentInfoCom.HeightHalfSize);
                if (camPos != pos) {
                    infoCom.SetPos(camPos);
                    pos = camPos;
                }
            }

            // Apply Shake
            Vector3 shakeOffset = ShakeCom.GetShakeOffset();
            mainCam.transform.position = pos + shakeOffset;

        }

    }

}