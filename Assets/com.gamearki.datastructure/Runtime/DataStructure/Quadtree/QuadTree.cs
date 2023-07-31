using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.DataStructure {

    internal interface Ptr_QuadTree {}

    public class QuadTree<T> : Ptr_QuadTree {

        // Config
        int maxDepth;
        public int MaxDepth => maxDepth;

        uint onlyIDRecord;

        QuadTreeNode<T> root;
        internal QuadTreeNode<T> Root => root;

        public QuadTree(float worldWidth, float worldHeight, int maxDepth) {
            if (maxDepth > 8) {
                throw new Exception("Max depth must be less than 8");
            }
            this.maxDepth = maxDepth;

            var bounds = new Bounds2(Vector2.zero, new Vector2(worldWidth, worldHeight));
            this.root = new QuadTreeNode<T>(this, bounds, 0);
            this.root.SetAsRoot();
        }

        // ==== API ====
        public void Traval(Action<QuadTreeNode<T>> action) {
            root.Traval(action);
        }

        public QuadTreeNode<T> Insert(T valuePtr, in Bounds2 bounds) {
            return root.Insert(valuePtr, bounds);
        }

        public void Remove(ulong fullID) {
            this.root.RemoveNode(fullID);
        }

        public void GetCandidateNodes(in Bounds2 bounds, HashSet<QuadTreeNode<T>> candidates) {
            this.root.GetCandidateNodes(bounds, candidates);
        }

        public void GetCandidateValues(in Bounds2 bounds, HashSet<T> candidates) {
            this.root.GetCandidateValues(bounds, candidates);
        }

        // ==== Internal ====
        internal uint GenOnlyID() {
            onlyIDRecord += 1;
            return onlyIDRecord;
        }

    }

}