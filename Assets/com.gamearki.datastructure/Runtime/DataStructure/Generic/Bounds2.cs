using UnityEngine;

namespace GameArki.DataStructure {

    public struct Bounds2 {

        public Vector2 min;
        public Vector2 max;

        public Vector2 Center => (min + max) * 0.5f;
        public Vector2 Size => max - min;

        public Bounds2(in Vector2 center, in Vector2 size) {
            var half = size * 0.5f;
            this.min = center - half;
            this.max = center + half;
        }

        public bool IsIntersectOrContains(in Bounds2 other) {
            return IsIntersect(other);
        }

        public bool IsIntersect(in Bounds2 other) {
            return (this.min.x <= other.max.x && this.max.x >= other.min.x
                && this.min.y <= other.max.y && this.max.y >= other.min.y);
        }

        public string ToFullString() {
            return $"center: {Center.ToString()}, size: {Size.ToString()}, min: {min.ToString()}, max: {max.ToString()}";
        }

    }

}