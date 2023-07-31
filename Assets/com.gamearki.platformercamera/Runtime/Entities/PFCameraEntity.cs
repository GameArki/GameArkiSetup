using UnityEngine;

namespace GameArki.PlatformerCamera.Entities {

    public class PFCameraEntity {

        int id;
        public int ID => id;
        public void SetID(int value) => id = value;

        PFCameraInfoComponent currentInfoCom;
        public PFCameraInfoComponent CurrentInfoCom => currentInfoCom;

        // ==== Constraints ====
        PFConfinerComponent confinerCom;
        public PFConfinerComponent ConfinerCom => confinerCom;

        // ==== State ====
        PFCameraFollowComponent followCom;
        public PFCameraFollowComponent FollowCom => followCom;

        PFCameraShakeStateComponent shakeCom;
        public PFCameraShakeStateComponent ShakeCom => shakeCom;

        public PFCameraEntity() {
            this.currentInfoCom = new PFCameraInfoComponent();
            this.followCom = new PFCameraFollowComponent();
            this.confinerCom = new PFConfinerComponent();
            this.shakeCom = new PFCameraShakeStateComponent();
        }

        public void TickEasing(float dt) {
            followCom.TickEasing(dt);
            shakeCom.TickEasing(dt);
        }

        public void Move(Vector3 offset) {
            currentInfoCom.Move(offset);
        }

        public void ShakeOnce(PFShakeStateModel arg) {
            shakeCom.ShakeOnce(arg);
        }

        public void ShakeSeveral(PFShakeStateModel[] args) {
            shakeCom.SetShake(args);
        }

    }

}