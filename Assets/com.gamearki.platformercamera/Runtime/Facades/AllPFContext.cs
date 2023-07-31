using UnityEngine;
using GameArki.PlatformerCamera.Repo;

namespace GameArki.PlatformerCamera.Facades {

    internal class AllPFContext {

        Camera mainCam;
        public Camera MainCam => mainCam;

        PFCameraRepo repo;
        public PFCameraRepo Repo => repo;

        internal AllPFContext() {
            this.repo = new PFCameraRepo();
        }

        internal void Inject(Camera main) {
            this.mainCam = main;
        }

    }

}