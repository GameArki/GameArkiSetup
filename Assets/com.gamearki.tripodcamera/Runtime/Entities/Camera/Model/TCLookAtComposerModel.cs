using GameArki.FPEasing;
using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public struct TCLookAtComposerModel {

        public TCLookAtComposerType composerType;
        public float screenNormalizedX;
        public float screenNormalizedY;
        public float deadZoneNormalizedW;
        public float deadZoneNormalizedH;
        public float softZoneNormalizedW;
        public float softZoneNormalizedH;
        public float normalDamping;
        public Vector3 normalLookAngles;
        public bool normalLookActivated;

        public Vector2 GetDeadZoneLT(float screenWidth, float screenHeight) {
            float screenX = screenNormalizedX * screenWidth;
            float screenY = screenNormalizedY * screenHeight;
            float deadZoneW = deadZoneNormalizedW * screenWidth;
            float deadZoneH = deadZoneNormalizedH * screenHeight;
            return new Vector2(screenX - deadZoneW / 2, screenY + deadZoneH / 2);
        }

        public Vector2 GetDeadZoneRB(float screenWidth, float screenHeight) {
            float screenX = screenNormalizedX * screenWidth;
            float screenY = screenNormalizedY * screenHeight;
            float deadZoneW = deadZoneNormalizedW * screenWidth;
            float deadZoneH = deadZoneNormalizedH * screenHeight;
            return new Vector2(screenX + deadZoneW / 2, screenY - deadZoneH / 2);
        }

        public bool IsInDeadZone(in Vector3 screenPoint, float screenWidth, float screenHeight) {
            return IsInZone(screenPoint, screenWidth, screenHeight, deadZoneNormalizedW, deadZoneNormalizedH, screenNormalizedX, screenNormalizedY);
        }

        public bool IsInDeadZone_Horizontal(in Vector3 screenPoint, float screenWidth, float screenHeight, out float pixelOffset) {
            return IsInZone_Horizontal(screenPoint, screenWidth, screenHeight, deadZoneNormalizedW, screenNormalizedX, out pixelOffset);
        }

        public bool IsInDeadZone_Vertical(in Vector3 screenPoint, float screenWidth, float screenHeight, out float pixelOffset) {
            return IsInZone_Vertical(screenPoint, screenWidth, screenHeight, deadZoneNormalizedH, screenNormalizedY, out pixelOffset);
        }

        bool IsInZone(in Vector3 screenPoint, float screenWidth, float screenHeight, float zoneNormalizedW, float zoneNormalizedH, float sx, float sy) {
            float width = screenWidth * zoneNormalizedW;
            float height = screenHeight * zoneNormalizedH;
            float screenX = sx * screenWidth;
            float screenY = sy * screenHeight;
            Vector2 lt = new Vector2(screenX - width / 2, screenY + height / 2);
            Vector2 rb = lt + new Vector2(width, -height);
            return screenPoint.z >= 0 && screenPoint.x >= lt.x && screenPoint.x <= rb.x && screenPoint.y <= lt.y && screenPoint.y >= rb.y;
        }

        bool IsInZone_Horizontal(in Vector3 screenPoint, float screenWidth, float screenHeight, float zoneNormalizedW, float screenNormalizedX, out float pixelOffset) {
            float width = screenWidth * zoneNormalizedW;
            float screenX = screenNormalizedX * screenWidth;
            float l = screenX - width / 2;
            float r = l + width;
            pixelOffset = screenPoint.x - screenX;
            return screenPoint.z >= 0 && l <= screenPoint.x && screenPoint.x <= r;
        }

        bool IsInZone_Vertical(in Vector3 screenPoint, float screenWidth, float screenHeight, float zoneNormalizedH, float screenNormalizedY, out float pixelOffset) {
            float height = screenHeight * zoneNormalizedH;
            float screenY = screenNormalizedY * screenHeight;
            float t = screenY + height / 2;
            float b = t - height;
            pixelOffset = screenPoint.y - screenY;
            return screenPoint.y >= b && screenPoint.y <= t;
        }

    }

}