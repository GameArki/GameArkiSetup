using UnityEngine;

namespace GameArki.TripodCamera {

    [System.Serializable]
    public struct TCWayPoint {

        public float x;
        public float y;
        public float z;
        public float r;

        public Vector3 position => new Vector3(x, y, z);

        public TCWayPoint(float x, float y, float z, float r) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.r = r;
        }

        public TCWayPoint(in Vector3 position, float r) {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
            this.r = r;
        }

        public void SetPosition(in Vector3 position) {
            this.x = position.x;
            this.y = position.y;
            this.z = position.z;
        }

    }

}
