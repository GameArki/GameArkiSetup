using System;

namespace GameArki.PathFinding.Generic {

    public struct Int2 {

        public int X;
        public int Y;

        public static Int2 Zero => new Int2(0, 0);

        public Int2(int x, int y) {
            X = x;
            Y = y;
        }

        public bool ValueEquals(Int2 v) {
            return X == v.X && Y == v.Y;
        }

        public static Int2 operator +(Int2 v1, Int2 v2) {
            return new Int2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Int2 operator -(Int2 v1, Int2 v2) {
            return new Int2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static bool operator ==(Int2 v1, Int2 v2) {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Int2 v1, Int2 v2) {
            return v1.X != v2.X || v1.Y != v2.Y;
        }

        public override string ToString() {
            return $"({X},{Y})";
        }

        public override bool Equals(object obj) {
            if (obj is Int2) {
                return ValueEquals((Int2)obj);
            }
            return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }
    }

}