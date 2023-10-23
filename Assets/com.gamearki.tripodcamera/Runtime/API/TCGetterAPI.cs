using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.API {

    internal class TCGetterAPI : ITCGetterAPI {

        TCContext context;
        TCDomain domain;

        internal TCGetterAPI() { }

        internal void Inject(TCContext context, TCDomain domain) {
            this.context = context;
            this.domain = domain;
        }

        bool ITCGetterAPI.Follow_HasTarget(int id) {
            var camDomain = domain.CameraDomain;
            return camDomain.Follow_HasTarget(id);
        }

        Transform ITCGetterAPI.Follow_GetTransform(int id) {
            var camDomain = domain.CameraDomain;
            return camDomain.Follow_GetTransform(id);
        }

        Vector3 ITCGetterAPI.Follow_GetNormalOffset(int id) {
            var camDomain = domain.CameraDomain;
            return camDomain.Follow_GetNormalOffset(id);
        }

        bool ITCGetterAPI.LookAt_HasTarget() {
            var curCam = context.CameraRepo.CurrentTCCam;
            if (curCam == null) {
                return false;
            }

            var targeterModel = curCam.TargetorModel;
            return targeterModel.HasLookAtTarget;
        }

        Vector2 ITCGetterAPI.LookAt_GetDeadZoneRB() {
            var tcCam = context.CameraRepo.CurrentTCCam;
            if (tcCam == null) {
                return Vector2.zero;
            }

            var composerModel = tcCam.LookAtComponent.model.composerModel;
            var beforeInfo = tcCam.BeforeInfo;
            var screenWidth = beforeInfo.ScreenWidth;
            var screenHeight = beforeInfo.ScreenHeight;
            return composerModel.GetDeadZoneRB(screenWidth, screenHeight);
        }

        Transform ITCGetterAPI.LookAt_GetTransform(int id) {
            var camDomain = domain.CameraDomain;
            return camDomain.LookAt_GetTransform(id);
        }

        Vector3 ITCGetterAPI.LookAt_GetNormalAngle(int id){
            var camDomain = domain.CameraDomain;
            return camDomain.LookAt_GetNormalAngle(id);
        }

        bool ITCGetterAPI.DollyTrack_IsActivated() {
            var curCam = context.CameraRepo.CurrentTCCam;
            if (curCam == null) {
                return false;
            }

            return curCam.DollyTrackStateComponent.IsActivated;
        }

        Vector3 ITCGetterAPI.WorldToScreenPoint(in Vector3 worldPosition,
                                                in Vector3 cameraPosition,
                                                in Quaternion cameraRotation,
                                                in Matrix4x4 projectionMatrix,
                                                int screenWidth,
                                                int screenHeight) {
            var camDomain = domain.CameraDomain;
            return camDomain.WorldToScreenPoint(worldPosition, cameraPosition, cameraRotation, projectionMatrix, screenWidth, screenHeight);
        }

        Vector3 ITCGetterAPI.WorldToViewportPoint(Vector3 worldPosition,
                                                  Vector3 cameraPosition,
                                                  Quaternion cameraRotation,
                                                  Matrix4x4 projectionMatrix) {
            var camDomain = domain.CameraDomain;
            return camDomain.WorldToViewportPoint(worldPosition, cameraPosition, cameraRotation, projectionMatrix);
        }

        Quaternion ITCGetterAPI.FromToScreenPoint(in Vector3 fromScreenPoint,
                                                  in Vector3 toScreenPoint,
                                                  in Matrix4x4 projectionMatrix,
                                                  int screenWidth,
                                                  int screenHeight) {
            var camDomain = domain.CameraDomain;
            return camDomain.FromToScreenPoint(fromScreenPoint, toScreenPoint, projectionMatrix, screenWidth, screenHeight);
        }

        Vector3 ITCGetterAPI.ScreenToViewportPoint(Vector3 screenPoint, int screenWidth, int screenHeight) {
            var camDomain = domain.CameraDomain;
            return camDomain.ScreenToViewportPoint(screenPoint, screenWidth, screenHeight);
        }

        Vector3 ITCGetterAPI.ScreenPointToDir(in Vector3 screenPoint,
                                              in Vector3 cameraPosition,
                                              in Quaternion cameraRotation,
                                              in Matrix4x4 projectionMatrix,
                                              int screenWidth,
                                              int screenHeight) {
            var camDomain = domain.CameraDomain;
            return camDomain.ScreenPointToDir(screenPoint, cameraPosition, cameraRotation, projectionMatrix, screenWidth, screenHeight);
        }

        Matrix4x4 ITCGetterAPI.GetProjectionMatrix(in Vector3 cameraPosition,
                                                   float fieldOfView,
                                                   float aspectRatio,
                                                   float nearClipPlane,
                                                   float farClipPlane) {
            var camDomain = domain.CameraDomain;
            return camDomain.GetProjectionMatrix(cameraPosition, fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
        }

        TCLookAtComposerType ITCGetterAPI.LookAt_GetComposerType() {
            var activeCam = context.CameraRepo.CurrentTCCam;
            return activeCam == null ? TCLookAtComposerType.None : activeCam.LookAtComponent.model.composerModel.composerType;
        }

        Vector2 ITCGetterAPI.LookAt_GetDeadZoneLT() {
            var tcCam = context.CameraRepo.CurrentTCCam;
            if (tcCam == null) {
                return Vector2.zero;
            }

            var composerModel = tcCam.LookAtComponent.model.composerModel;
            var beforeInfo = tcCam.BeforeInfo;
            var screenWidth = beforeInfo.ScreenWidth;
            var screenHeight = beforeInfo.ScreenHeight;
            return composerModel.GetDeadZoneLT(screenWidth, screenHeight);
        }

    }

}