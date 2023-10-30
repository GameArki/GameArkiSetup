using GameArki.FPEasing;
using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Entities;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCLookAtAPI : ITCLookAtAPI {

        TCLookAtDomain lookAtDomain;

        public TCLookAtAPI() { }

        public void Inject(TCLookAtDomain lookAtDomain) {
            this.lookAtDomain = lookAtDomain;
        }

        TCLookAtComposerType ITCLookAtAPI.GetComposerType(int id) {
            return lookAtDomain.GetComposerType(id);
        }

        Vector2 ITCLookAtAPI.GetDeadZoneLT(int id) {
            return lookAtDomain.GetDeadZoneLT(id);
        }

        Vector2 ITCLookAtAPI.GetDeadZoneRB(int id) {
            return lookAtDomain.GetDeadZoneRB(id);
        }

        Vector3 ITCLookAtAPI.GetNormalAngle(int id) {
            return lookAtDomain.GetNormalAngle(id);
        }

        bool ITCLookAtAPI.HasTarget(int id) {
            return lookAtDomain.HasTarget(id);
        }

        void ITCLookAtAPI.SetComposer(in TCLookAtComposerModel composerModel, int id) {
            lookAtDomain.SetComposer(composerModel, id);
        }

        void ITCLookAtAPI.SetComposerNormalAngles(in Vector3 eulerAngles, int id) {
            lookAtDomain.SetComposerNormalAngles(eulerAngles, id);
        }

        void ITCLookAtAPI.SetComposerNormalDamping(float damping, int id) {
            lookAtDomain.SetComposerNormalDamping(damping, id);
        }

        void ITCLookAtAPI.SetComposerNormalLookActivated(bool activated, int id) {
            lookAtDomain.SetComposerNormalLookActivated(activated, id);
        }

        void ITCLookAtAPI.SetComposerType(TCLookAtComposerType composerType, int id) {
            lookAtDomain.SetComposerType(composerType, id);
        }

        void ITCLookAtAPI.SetInit(in Vector3 offset, EasingType horizontalEasingType, float horizontalEasingTime, EasingType verticalEasingType, float verticalEasingTime, int id) {
            lookAtDomain.SetInit(offset, horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime, id);
        }

        void ITCLookAtAPI.SetEasing(EasingType horizontalEasingType, float horizontalEasingTime, EasingType verticalEasingType, float verticalEasingTime, int id) {
            lookAtDomain.SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime, id);
        }

        void ITCLookAtAPI.SetEnabled(bool enabled, int id) {
            lookAtDomain.SetEnabled(enabled, id);
        }

        void ITCLookAtAPI.SetNormalAngles(in Vector3 eulerAngles, int id) {
            lookAtDomain.SetNormalAngles(eulerAngles, id);
        }

        void ITCLookAtAPI.SetNormalLookActivated(bool activated, int id) {
            lookAtDomain.SetNormalLookActivated(activated, id);
        }

        void ITCLookAtAPI.TickLookAtPos(Vector3 pos, int id) {
            lookAtDomain.TickLookAtPos(pos, id);
        }

        void ITCLookAtAPI.CancelLookAt(int id) {
            lookAtDomain.CancelLookAt(id);
        }
    }

}