using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public static class DictionaryKeyUtil {

        public static ulong ComputeRBKey(in FPRigidbody2DEntity a, in FPRigidbody2DEntity b) {
            uint aID = a.ID;
            uint bID = b.ID;
            uint minor;
            uint major;
            if (aID > bID) {
                major = aID;
                minor = bID;
            } else {
                major = bID;
                minor = aID;
            }
            ulong v = minor;
            v |= (ulong)major << 32;
            return v;
        }

    }
}