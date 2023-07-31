using UnityEngine;

namespace GameArki.PlatformerCamera.Entities {

    public class PFCameraInfoComponent {

        Vector3 pos;
        public Vector3 Pos => pos;
        public void SetPos(Vector3 value) => pos = value;

        float heightHalfSize;
        public float HeightHalfSize => heightHalfSize;
        public void SetHeightHalfSize(float value) => heightHalfSize = value;

        public PFCameraInfoComponent() {}

        internal void Move(Vector3 offset) {
            this.pos += offset;
        }

        internal Vector2 GetViewSize(float screenRadio) {
            var height = heightHalfSize * 2;
            var width = screenRadio * height;
            return new Vector2(width, height);
        }

    }

}