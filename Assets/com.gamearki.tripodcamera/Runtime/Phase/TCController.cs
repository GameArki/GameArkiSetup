using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.Controller {

    internal class TCController {

        TCContext tcContext;
        TCDomain domain;

        internal TCController() { }

        internal void Inject(TCContext context, TCDomain domain) {
            this.tcContext = context;
            this.domain = domain;
        }

        internal void Init() {

        }

        internal void Tick(float dt) {
            var applyDomain = domain.ApplyDomain;

            // - All cameras logic.
            var camRepo = tcContext.CameraRepo;
            camRepo.ForeachAll((cam) => {
                if (!cam.Activated) return;
                applyDomain.TickAndApply(cam, dt);
                applyDomain.Tick_StateEffect(cam, dt);
#if UNITY_EDITOR
                Color color = cam == camRepo.CurrentTCCam ? Color.green : Color.gray;
                color = cam.ID == 2 ? Color.red : color;
                Debug.DrawLine(cam.AfterInfo.Position, cam.AfterInfo.Position + cam.AfterInfo.Rotation * Vector3.forward * 100f, color);
#endif                                                                                                                      
            });

            // - Director FSM.
            var directorDomain = domain.DirectorDomain;
            directorDomain.TickFSM(dt);

            var mainCam = tcContext.MainCamera;
            var curTCCamera = camRepo.CurrentTCCam;
            var directorFSMState = tcContext.directorEntity.FSMComponent.FSMState;
            applyDomain.ApplyToMain(curTCCamera, mainCam);
        }

    }

}