namespace GameArki.GenGen {

    public struct Vec2Int {

        public static Vec2Int zero => new Vec2Int() { x = 0, y = 0 };

        public int x;
        public int y;

        public Vec2Int(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Vec2Int(int v) {
            this.x = v;
            this.y = v;
        }

        public Vec2Int(Vec2Int v) {
            this.x = v.x;
            this.y = v.y;
        }

        public int ToArrayIndex(int width) {
            return y * width + x;
        }

        public static Vec2Int FromArrayIndex(int index, int width) {
            return new Vec2Int(index % width, index / width);
        }

        public static Vec2Int operator +(Vec2Int a, Vec2Int b) {
            return new Vec2Int(a.x + b.x, a.y + b.y);
        }

        public static Vec2Int operator -(Vec2Int a, Vec2Int b) {
            return new Vec2Int(a.x - b.x, a.y - b.y);
        }

        public static Vec2Int operator *(Vec2Int a, int b) {
            return new Vec2Int(a.x * b, a.y * b);
        }

        public static Vec2Int operator /(Vec2Int a, int b) {
            return new Vec2Int(a.x / b, a.y / b);
        }

        public override string ToString() {
            return string.Format("({0}, {1})", x, y);
        }

    }
}