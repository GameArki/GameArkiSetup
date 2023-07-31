using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.DataStructure {

    internal interface Ptr_Octree {}

    public class Octree<T> : Ptr_Octree {

        int maxDepth;
        public int MaxDepth => maxDepth;

        uint onlyIDRecord;

        OctreeNode<T> root;

        public Octree(float worldWidth, float worldHeight, float worldLength, int maxDepth) {
            if (maxDepth > 8) {
                throw new Exception("Max depth must be less than 8");
            }

            this.maxDepth = maxDepth;
            this.root = new OctreeNode<T>(this, new Bounds3(Vector3.zero, new Vector3(worldWidth, worldHeight, worldLength)), 0);
            this.root.SetAsRoot();
        }

        internal uint GenOnlyID() {
            onlyIDRecord += 1;
            return onlyIDRecord;
        }

        public OctreeNode<T> Insert(T value, Bounds3 bounds) {
            return root.Insert(value, bounds);
        }

        public void Remove(uint128 fullID) {
            root.RemoveNode(fullID);
        }

        public void GetCandidateNodes(in Bounds3 bounds, HashSet<OctreeNode<T>> candidates) {
            root.GetCandidateNodes(bounds, candidates);
        }

        public void GetCandidateValues(in Bounds3 bounds, HashSet<T> candidates) {
            root.GetCandidateValues(bounds, candidates);
        }

        public void Traval(Action<OctreeNode<T>> action) {
            root.Traval(action);
        }

    }

}