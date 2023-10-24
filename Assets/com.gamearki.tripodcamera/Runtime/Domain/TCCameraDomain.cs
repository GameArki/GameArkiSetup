using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.Domain {

    public class TCCameraDomain {

        TCContext context;

        public TCCameraDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public int Spawn(Vector3 pos, Quaternion rot, float fov) {
            var repo = context.CameraRepo;
            var mainCam = context.MainCamera;
            var tcCam = new TCCameraEntity();
            var camID = context.FetchCameraID();
            tcCam.SetID(camID);
            tcCam.InitInfo(pos, rot, fov, mainCam.aspect, mainCam.nearClipPlane, mainCam.farClipPlane, Screen.width, Screen.height);
            if (!repo.TryAdd(tcCam)) {
                Debug.LogError($"[TCCameraDomain] Spawn: Failed to add camera to repo. ID: {camID}");
                return -1;
            }

            Debug.Log($"[TCCameraDomain] Spawn: {camID}");
            return camID;
        }

        public bool SetActive(bool active, int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var cam)) {
                Debug.LogError($"[TCCameraDomain] SetActiveTCCamera: Failed to get camera from repo. ID: {id}");
                return false;
            }

            cam.SetActivated(active);
            return true;
        }

        public void SetPosition(in Vector3 pos, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.SetPosition(pos);
        }

        public void SetRotation(in Quaternion rot, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.SetRotation(rot);
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

        public void Remove(int id) {
            var repo = context.CameraRepo;
            repo.Remove(id);
        }

        public TCCameraEntity GetTCCamera(int id) {
            TCCameraEntity tcCam;
            var repo = context.CameraRepo;
            if (id == -1) {
                tcCam = repo.CurrentTCCam;
            } else {
                repo.TryGet(id, out tcCam);
            }

            return tcCam;
        }

        public void ApplyTM(in TCCameraTM tm, int id = -1) {
            TCCameraEntity tc = null;
            if (id == -1) {
                tc = context.CameraRepo.CurrentTCCam;
            } else {
                context.CameraRepo.TryGet(id, out tc);
            }
            if (tc == null) return;

            if (tm.needSet_Follow) tc.FollowComponent.model = TCTM2RuntimeUtil.ToTCFollowModel(tm.followTM);

            if (tm.needSet_LookAt) tc.LookAtComponent.model = TCTM2RuntimeUtil.ToTCLookAtModel(tm.lookAtTM);

            if (tm.needSet_Misc) tc.MISCComponent.model = TCTM2RuntimeUtil.ToTCMiscModel(tm.miscTM);

            if (tm.needSet_Movement) tc.MovementStateCom.Enter(TCTM2RuntimeUtil.ToTCMovementStateModelArray(tm.movementStateTMArray), tm.isExitReset_Movement, tm.exitEasing_Movement, tm.exitDuration_Movement);

            if (tm.needSet_Round) tc.RoundStateCom.Enter(TCTM2RuntimeUtil.ToTCRoundStateModelArray(tm.roundStateTMArray), tm.isExitReset_Round, tm.exitEasing_Round, tm.exitDuration_Round);

            if (tm.needSet_Rotate) tc.RotateStateComponent.Enter(TCTM2RuntimeUtil.ToTCRotateStateModelArray(tm.rotateStateTMArray), tm.isExitReset_Rotate, tm.exitEasing_Rotate, tm.exitDuration_Rotate);

            if (tm.needSet_Push) tc.PushStateCom.Enter(TCTM2RuntimeUtil.ToTCPushStateModelArray(tm.pushStateTMArray), tm.isExitReset_Push, tm.exitEasing_Push, tm.exitDuration_Push);

            if (tm.needSet_FOV) tc.FOVStateComponent.Enter(TCTM2RuntimeUtil.ToTCFOVStateModelArray(tm.fovStateTMArray), tm.isExitReset_FOV, tm.exitEasing_FOV, tm.exitDuration_FOV);

            if (tm.needSet_DollyTrack) tc.DollyTrackStateComponent.Enter(TCTM2RuntimeUtil.ToTCDollyTrackStateModel(tm.dollyTrackStateTM));

            if (tm.needSet_Shake) tc.ShakeStateComponent.Enter(TCTM2RuntimeUtil.ToTCShakeStateModelArray(tm.shakeStateTMArray));
        }

    }

}