using GameArki.FPEasing;
using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Template;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCDirectorAPI : ITCDirectorAPI {

        TCDirectorDomain directorDomain;

        public TCDirectorAPI() { }

        public void Inject(TCDirectorDomain directorDomain) {
            this.directorDomain = directorDomain;
        }

        bool ITCDirectorAPI.BlendToTCCamera(EasingType easingType, float duration, int id) {
            return directorDomain.Enter_Blend(easingType, duration, id);
        }

        bool ITCDirectorAPI.CutToTCCamera(int id) {
            return directorDomain.CutToTCCamera(id);
        }
    }

}