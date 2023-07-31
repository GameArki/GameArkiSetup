using System.Threading.Tasks;
using UnityEngine;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.Facades {

    public class TCContext {

        Camera camera;
        public Camera MainCamera => camera;

        TCCameraRepo cameraRepo;
        public TCCameraRepo CameraRepo => cameraRepo;

        TCCameraHookRepo hookRepo;
        public TCCameraHookRepo HookRepo => hookRepo;

        int cameraID;
        public int FetchCameraID() => cameraID++;

        public TCDirectorEntity directorEntity;

        public TCContext() {
            this.cameraRepo = new TCCameraRepo();
            this.hookRepo = new TCCameraHookRepo();
            this.directorEntity = new TCDirectorEntity();
        }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

    }

}