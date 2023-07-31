using UnityEngine;
using UnityEngine.UI;

namespace GameArki.DataStructure {

    public struct Bounds3 {

        public Vector3 min;
        public Vector3 max;

        public Vector3 Center => (min + max) * 0.5f;
        public Vector3 Size => max - min;

        public Bounds3(in Vector3 center, in Vector3 size) {
            var half = size * 0.5f;
            this.min = center - half;
            this.max = center + half;
        }

        public bool IsIntersectOrContains(in Bounds3 other) {
            return IsIntersect(other) || IsContains(other);
        }

        public bool IsIntersect(in Bounds3 other) {
            return this.min.x <= other.max.x && this.max.x >= other.min.x &&
                   this.min.y <= other.max.y && this.max.y >= other.min.y &&
                   this.min.z <= other.max.z && this.max.z >= other.min.z;
        }

        public bool IsContains(in Bounds3 other) {
            return (this.min.x <= other.min.x && this.max.x >= other.max.x &&
                    this.min.y <= other.min.y && this.max.y >= other.max.y &&
                    this.min.z <= other.min.z && this.max.z >= other.max.z) ||
                   (this.min.x >= other.min.x && this.max.x <= other.max.x &&
                    this.min.y >= other.min.y && this.max.y <= other.max.y &&
                    this.min.z >= other.min.z && this.max.z <= other.max.z);
        }

        public string ToFullString() {
            return $"center: {Center.ToString()}, size: {Size.ToString()}, min: {min.ToString()}, max: {max.ToString()}";
        }

    }

}