using System;
using UnityEngine;

namespace GameArki.PlatformerCamera {

    public class PFConfinerComponent {

        bool isEnable;
        public bool IsEnable => isEnable;

        Vector2 worldMin;
        public Vector2 WorldMin => worldMin;

        Vector2 worldMax;
        public Vector2 WorldMax => worldMax;

        public PFConfinerComponent() { }

        public void SetConfiner(bool isEnable, Vector2 worldMin, Vector2 worldMax) {
            this.worldMin = worldMin;
            this.worldMax = worldMax;
            this.isEnable = isEnable;
        }

        public void SetEnable(bool value) {
            this.isEnable = value;
        }

        public Vector2 GetSize() {
            return worldMax - worldMin;
        }

        public Vector3 LockCameraInside(Vector3 camPos, float orthographicSize) {

            Vector3 resPos = camPos;
            float radio = (float)Screen.currentResolution.width / Screen.currentResolution.height;
            Vector2 halfSize = new Vector2(orthographicSize * radio, orthographicSize);
            Vector2 camWorldMin = (Vector2)camPos - halfSize;
            Vector2 camWorldMax = (Vector2)camPos + halfSize;

            if (camWorldMin.x < worldMin.x) {
                float xDiff = worldMin.x - camWorldMin.x;
                resPos.x += xDiff;
            } else if (camWorldMax.x > worldMax.x) {
                float xDiff = worldMax.x - camWorldMax.x;
                resPos.x += xDiff;
            }

            if (camWorldMin.y < worldMin.y) {
                float yDiff = worldMin.y - camWorldMin.y;
                resPos.y += yDiff;
            } else if (camWorldMax.y > worldMax.y) {
                float yDiff = worldMax.y - camWorldMax.y;
                resPos.y += yDiff;
            }

            return resPos;

        }

        internal Vector3 GetCenter() {
            return (worldMin + worldMax) * 0.5f;
        }
    }

}