using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    public interface ITCGetterAPI {

        // ======== Follow ========
        bool Follow_HasTarget(int id);
        Transform Follow_GetTransform(int id);
        Vector3 Follow_GetNormalOffset(int id);

        // ======== LookAt ========
        bool LookAt_HasTarget();
        TCLookAtComposerType LookAt_GetComposerType();
        Vector2 LookAt_GetDeadZoneLT();
        Vector2 LookAt_GetDeadZoneRB();
        Transform LookAt_GetTransform(int id);
        Vector3 LookAt_GetNormalAngle(int id);

        // ======== Dolly ========
        bool DollyTrack_IsActivated();

        // ======== Common ========
        Quaternion FromToScreenPoint(in Vector3 fromScreenPoint,
                                     in Vector3 toScreenPoint,
                                     in Matrix4x4 projectionMatrix,
                                     int screenWidth,
                                     int screenHeight);

        Vector3 WorldToScreenPoint(in Vector3 worldPosition,
                                   in Vector3 cameraPosition,
                                   in Quaternion cameraRotation,
                                   in Matrix4x4 projectionMatrix,
                                   int screenWidth,
                                   int screenHeight);

        Vector3 WorldToViewportPoint(Vector3 worldPosition,
                                     Vector3 cameraPosition,
                                     Quaternion cameraRotation,
                                     Matrix4x4 projectionMatrix);

        Vector3 ScreenToViewportPoint(Vector3 screenPoint, int screenWidth, int screenHeight);

        Vector3 ScreenPointToDir(in Vector3 screenPoint,
                                 in Vector3 cameraPosition,
                                 in Quaternion cameraRotation,
                                 in Matrix4x4 projectionMatrix,
                                 int screenWidth,
                                 int screenHeight);
        Matrix4x4 GetProjectionMatrix(in Vector3 cameraPosition,
                                      float fieldOfView,
                                      float aspectRatio,
                                      float nearClipPlane,
                                      float farClipPlane);

    }

}