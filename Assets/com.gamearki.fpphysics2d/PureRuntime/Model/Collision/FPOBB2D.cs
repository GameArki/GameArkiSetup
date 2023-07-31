using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPOBB2D {

        FPVector2 axisX;
        public FPVector2 AxisX => axisX;
        FPVector2 axisY;
        public FPVector2 AxisY => axisY;

        FPVector2 center;
        public FPVector2 Center => center;

        FPVector2 size;
        public FPVector2 Size => size;

        FPVector2[] vertices;
        public FPVector2[] Vertices => vertices;

        FP64 radAngle;
        public FP64 RadAngle => radAngle;

        public FPOBB2D(in FPVector2 center, in FPVector2 size, in FP64 radAngle) {

            this.center = center;
            this.size = size;
            this.radAngle = radAngle;

            if (radAngle == FP64.Zero) {

                // Fake OBB
                this.axisY = FPVector2.UnitY;
                this.axisX = FPVector2.UnitX;

                FPVector2 half = size * FP64.Half;
                FPVector2 ax = axisX * half.x;
                FPVector2 ay = axisY * half.y;
                var vertices = new FPVector2[4];
                vertices[0] = center + -half;
                vertices[1] = center + new FPVector2(-half.x, half.y);
                vertices[2] = center + half;
                vertices[3] = center + new FPVector2(half.x, -half.y);
                this.vertices = vertices;

            } else {

                // OBB
                FPRotation2D rot = new FPRotation2D(radAngle);

                this.axisY = FPMath2DUtil.MulRotAndPos(rot, FPVector2.UnitY);
                this.axisX = FPMath2DUtil.MulRotAndPos(rot, FPVector2.UnitX);

                FPVector2 half = size * FP64.Half;
                FPVector2 ax = axisX * half.x;
                FPVector2 ay = axisY * half.y;
                var vertices = new FPVector2[4];
                vertices[0] = center + (-ax + -ay);
                vertices[1] = center + (-ax + ay);
                vertices[2] = center + (ax + ay);
                vertices[3] = center + (ax + -ay);
                this.vertices = vertices;

            }

        }

    }

}