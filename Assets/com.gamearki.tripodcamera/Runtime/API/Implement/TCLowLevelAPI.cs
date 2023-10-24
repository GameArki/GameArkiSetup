using GameArki.TripodCamera.Domain;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCLowLevelAPI : ITCLowLevelAPI {

        TCLowLevelDomain lowLevelDomain;

        public TCLowLevelAPI() { }

        public void Inject(TCLowLevelDomain lowLevelDomain) {
            this.lowLevelDomain = lowLevelDomain;
        }

        void ITCLowLevelAPI.Move(Vector2 value, int id) {
            lowLevelDomain.Move(value, id);
        }

        void ITCLowLevelAPI.Move_AndChangeLookAtOffset(Vector2 value, int id) {
            lowLevelDomain.Move_AndChangeLookAtOffset(value, id);
        }

        void ITCLowLevelAPI.Push(float value, int id) {
            lowLevelDomain.Push(value, id);
        }

        void ITCLowLevelAPI.Rotate_Roll(float z, int id) {
            lowLevelDomain.Rotate_Roll(z, id);
        }

        void ITCLowLevelAPI.Zoom(float value, int id) {
            lowLevelDomain.Zoom(value, id);
        }
    }

}