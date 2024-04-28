using System;

namespace GameArki.PathFinding.Generic {

    public class AStarNode : IComparable<AStarNode> {

        public int f;
        public int g;
        public int h;
        public Int2 pos;
        public AStarNode parent;
public static int count ;
        public AStarNode() {
            UnityEngine.Debug.Log("AStarNode count: " + ++count);
        }

        public int CompareTo(AStarNode x) {
            if (x.f > f) {
                return -1;
            } else if (x.f < f) {
                return 1;
            } else {
                return 0;
            }
        }

        public void Clear() {
            f = 0;
            g = 0;
            h = 0;
            pos = Int2.Zero;
            parent = null;
        }

    }

}