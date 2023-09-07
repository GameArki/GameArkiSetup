using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.Controller {

    internal class TCController {

        TCContext context;
        TCDomain domain;

        internal TCController() { }

        internal void Inject(TCContext context, TCDomain domain) {
            this.context = context;
            this.domain = domain;
        }

        internal void Init() {

        }

        internal void Tick(float dt) {

            var applyDomain = domain.ApplyDomain;

            var hooks = context.HookRepo.GetAll();
            foreach (var hook in hooks) {
                applyDomain.ApplyHook(hook);
            }

            // - All cameras logic.
            var camRepo = context.CameraRepo;
            camRepo.ForeachAll((cam) => {
                applyDomain.ApplyNormal(cam, dt);
                applyDomain.ApplyStateEffect(cam, dt);
            });

            // - Director FSM.
            var directorDomain = domain.DirectorDomain;
            directorDomain.TickFSM(dt);

            var mainCam = context.MainCamera;
            var curTCCamera = camRepo.CurrentTCCam;
            applyDomain.ApplyToMain(curTCCamera, mainCam);
        }

    }

}