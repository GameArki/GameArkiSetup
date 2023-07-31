using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Hook;

namespace GameArki.TripodCamera.API {

    public interface ITCGetterAPI {
        
        bool HasFollowTarget();
        Transform GetFollowTransform(int id);

        bool HasLookAtTarget();
        TCLookAtComposerType GetLookAtComposerType();
        Vector2 GetLookAtDeadZoneLT_LookAt();
        Vector2 GetLookAtDeadZoneRB_LookAt();
        Transform GetLookAtTransform(int id);

        bool IsDollyTrackActivated();

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