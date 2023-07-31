using System;
using System.Collections.Generic;
using FixMath.NET;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace GameArki.FPDataStructure {

    internal interface Ptr_FPQuadTree {}

    public class FPQuadTree<T> : Ptr_FPQuadTree {

        // Config
        int maxDepth;
        public int MaxDepth => maxDepth;

        uint onlyIDRecord;

        FPQuadTreeNode<T> root;
        internal FPQuadTreeNode<T> Root => root;

        public FPQuadTree(FP64 worldWidth, FP64 worldHeight, int maxDepth) {
            if (maxDepth > 8) {
                throw new Exception("Max depth must be less than 8");
            }
            this.maxDepth = maxDepth;

            var bounds = new FPBounds2(FPVector2.Zero, new FPVector2(worldWidth, worldHeight));
            this.root = new FPQuadTreeNode<T>(this, bounds, 0);
            this.root.SetAsRoot();
        }

        // ==== API ====
        public void Traval(Action<FPQuadTreeNode<T>> action) {
            root.Traval(action);
        }

        public FPQuadTreeNode<T> Insert(T valuePtr, in FPBounds2 bounds) {
            return root.Insert(valuePtr, bounds);
        }

        public void Remove(ulong fullID) {
            this.root.RemoveNode(fullID);
        }

        public void GetCandidateNodes(in FPBounds2 bounds, List<FPQuadTreeNode<T>> candidates) {
            this.root.GetCandidateNodes(bounds, candidates);
        }

        public void GetCandidateValues(in FPBounds2 bounds, List<T> candidates) {
            this.root.GetCandidateValues(bounds, candidates);
        }

        // ==== Internal ====
        internal uint GenOnlyID() {
            onlyIDRecord += 1;
            return onlyIDRecord;
        }

#if UNITY_EDITOR
        public void Editor_DrawGizmos(Color color, bool isXZ = false) {
            Gizmos.color = color;
            Traval(value => {
                var center = value.Bounds.Center;
                var size = value.Bounds.Size;
                Vector2 center2 = new Vector2((float)center.x.AsFloat(), (float)center.y.AsFloat());
                Vector2 size2 = new Vector2((float)size.x.AsFloat(), (float)size.y.AsFloat());
                if (isXZ) {
                    Gizmos.DrawWireCube(new Vector3(center2.x, 0, center2.y), new Vector3(size2.x, 0, size2.y));
                } else {
                    Gizmos.DrawWireCube(center2, size2);
                }
            });
        }
#endif
    }

}