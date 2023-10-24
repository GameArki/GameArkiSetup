using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCMathAPI : ITCMathAPI {

        public TCMathAPI() { }

        public void Inject() {
        }

        Quaternion ITCMathAPI.FromToScreenPoint(in Vector3 fromScreenPoint, in Vector3 toScreenPoint, in Matrix4x4 projectionMatrix, int screenWidth, int screenHeight) {
            return TCMathUtil.FromToScreenPoint(fromScreenPoint, toScreenPoint, projectionMatrix, screenWidth, screenHeight);
        }

        Matrix4x4 ITCMathAPI.GetProjectionMatrix(in Vector3 cameraPosition, float fieldOfView, float aspectRatio, float nearClipPlane, float farClipPlane) {
            return TCMathUtil.GetProjectionMatrix(cameraPosition, fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
        }

        Vector3 ITCMathAPI.ScreenPointToDir(in Vector3 screenPoint, in Vector3 cameraPosition, in Quaternion cameraRotation, in Matrix4x4 projectionMatrix, int screenWidth, int screenHeight) {
            return TCMathUtil.ScreenPointToDir(screenPoint, cameraPosition, cameraRotation, projectionMatrix, screenWidth, screenHeight);
        }

        Vector3 ITCMathAPI.ScreenToViewportPoint(Vector3 screenPoint, int screenWidth, int screenHeight) {
            return TCMathUtil.ScreenToViewportPoint(screenPoint, screenWidth, screenHeight);
        }

        Vector3 ITCMathAPI.WorldToScreenPoint(in Vector3 worldPosition, in Vector3 cameraPosition, in Quaternion cameraRotation, in Matrix4x4 projectionMatrix, int screenWidth, int screenHeight) {
            return TCMathUtil.WorldToScreenPoint(worldPosition, cameraPosition, cameraRotation, projectionMatrix, screenWidth, screenHeight);
        }

        Vector3 ITCMathAPI.WorldToViewportPoint(in Vector3 worldPosition, in Vector3 cameraPosition, in Quaternion cameraRotation, in Matrix4x4 projectionMatrix) {
            return TCMathUtil.WorldToViewportPoint(worldPosition, cameraPosition, cameraRotation, projectionMatrix);
        }
        
    }

}