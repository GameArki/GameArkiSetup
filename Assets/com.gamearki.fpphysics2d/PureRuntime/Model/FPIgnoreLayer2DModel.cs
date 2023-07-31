using System.Collections.Generic;

namespace GameArki.FPPhysics2D {

    public class FPIgnoreLayer2DModel {

        HashSet<ulong> all;

        public FPIgnoreLayer2DModel() {
            this.all = new HashSet<ulong>();
        }

        public void Ignore(int layer1, int layer2) {
            ulong key = Combine(layer1, layer2);
            all.Add(key);
        }

        public void CancelIgnore(int layer1, int layer2) {
            ulong key = Combine(layer1, layer2);
            all.Remove(key);
        }

        public bool IsIgnore(int layer1, int layer2) {
            ulong key = Combine(layer1, layer2);
            return all.Contains(key);
        }

        ulong Combine(int layer1, int layer2) {
            int major;
            int minor;
            if (layer1 > layer2) {
                major = layer1;
                minor = layer2;
            } else {
                major = layer2;
                minor = layer1;
            }
            ulong value = (ulong)(uint)minor;
            value |= (ulong)(uint)major << 32;
            return value;
        }

    }

}