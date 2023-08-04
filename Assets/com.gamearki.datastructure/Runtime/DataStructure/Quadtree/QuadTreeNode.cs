using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.DataStructure {

    // 插入: 按 Bounds 插入
    // 移除: 按 FullID 移除
    // 查询: 按 Bounds 查询
    // 遍历: 全遍历
    public class QuadTreeNode<T> {

        // ==== Define ====
        static class LocationConfig {
            // 3 4
            // 1 2
            public const byte NONE = 0b0000;
            public const byte BL = 0b0001;
            public const byte BR = 0b0010;
            public const byte TL = 0b0100;
            public const byte TR = 0b1000;
            public const byte FULL = 0b1111;

            public const int DEPTH_SHIFT = 4;
        }

        static Dictionary<byte, int> cornerToIndex = new Dictionary<byte, int>() {
            { LocationConfig.BL, BL_INDEX },
            { LocationConfig.BR, BR_INDEX },
            { LocationConfig.TL, TL_INDEX },
            { LocationConfig.TR, TR_INDEX },
        };

        const int BL_INDEX = 0;
        const int BR_INDEX = 1;
        const int TL_INDEX = 2;
        const int TR_INDEX = 3;

        // ==== External ====
        Ptr_QuadTree treePtr;
        QuadTree<T> Tree => treePtr as QuadTree<T>;

        // ==== Info ====
        uint locationID;
        void SetLocationID(uint value) => locationID = value;

        uint onlyID;
        public ulong GetFullID() => ((ulong)onlyID << 32) | (ulong)locationID;

        object valuePtr;
        public T Value => (T)valuePtr;

        Bounds2 bounds;
        public Bounds2 Bounds => bounds;

        int depth;
        void SetDepth(int value) => depth = value;

        bool isSplit;

        // 存储叶
        Dictionary<uint, QuadTreeNode<T>> children;

        // 分割 四块分支
        QuadTreeNode<T>[] splitedArray; // len: 4

        internal QuadTreeNode(Ptr_QuadTree tree, in Bounds2 bounds, int depth) {
            this.treePtr = tree;
            this.isSplit = false;
            this.bounds = bounds;
            this.depth = depth;
            this.children = new Dictionary<uint, QuadTreeNode<T>>();
            this.splitedArray = new QuadTreeNode<T>[4];
        }

        internal void SetAsRoot() {
            this.locationID = LocationConfig.FULL;
        }

        // ==== Generic ====
        uint GenBranchLocationID(uint parentLocationID, byte corner, int parentDepth) {
            uint value = parentLocationID | ((uint)corner << ((parentDepth + 1) * LocationConfig.DEPTH_SHIFT));
            return value;
        }

        uint GenLeafLocationID(uint parentLocationID, int parentDepth, byte cornerID) {
            return parentLocationID |= ((uint)cornerID << ((parentDepth) * LocationConfig.DEPTH_SHIFT));
        }

        uint GetLocationIDFromFullID(ulong fullID) {
            return (uint)(fullID & 0xFFFFFFFF);
        }

        byte GetCornerIDFromLoactionID(uint locationID, int depth) {
            int shift = (depth) * LocationConfig.DEPTH_SHIFT;
            uint loc = locationID & ((uint)LocationConfig.FULL << shift);
            return (byte)(loc >> shift);
        }

        byte GetCornerIDFromFullID(ulong fullID, int depth) {
            int shift = (depth) * LocationConfig.DEPTH_SHIFT;
            ulong loc = fullID & ((ulong)LocationConfig.FULL << shift);
            return (byte)(loc >> shift);
        }

        void SetAsLeaf(T valuePtr) {
            this.valuePtr = valuePtr;
        }

        void SetAsBranch() {
            this.valuePtr = null;
        }

        bool IsLeaf() {
            return valuePtr != null;
        }

        bool IsIntersectOrContains(in Bounds2 other) {
            return bounds.IsIntersect(other);
        }

        bool AndNotZero(byte a, byte b) {
            return (a & b) != 0;
        }

        // ==== Traval ====
        internal void Traval(Action<QuadTreeNode<T>> action) {

            action.Invoke(this);

            for (int i = 0; i < splitedArray.Length; i += 1) {
                var corner = splitedArray[i];
                if (corner != null) {
                    corner.Traval(action);
                }
            }

            foreach (var kv in children) {
                action.Invoke(kv.Value);
            }

        }

        void VisitCorner(byte corner, Action<QuadTreeNode<T>> action) {

            if (corner == LocationConfig.NONE) {
                return;
            }

            if (corner == LocationConfig.FULL) {
                for (int i = 0; i < splitedArray.Length; i += 1) {
                    var cornerNode = splitedArray[i];
                    if (cornerNode != null) {
                        action.Invoke(cornerNode);
                    }
                }
                return;
            }

            bool has = cornerToIndex.TryGetValue(corner, out int index);
            if (has) {
                var cornerNode = splitedArray[index];
                if (cornerNode != null) {
                    action.Invoke(cornerNode);
                }
            } else {
                if (AndNotZero(corner, LocationConfig.BL)) {
                    action.Invoke(splitedArray[BL_INDEX]);
                }
                if (AndNotZero(corner, LocationConfig.BR)) {
                    action.Invoke(splitedArray[BR_INDEX]);
                }
                if (AndNotZero(corner, LocationConfig.TL)) {
                    action.Invoke(splitedArray[TL_INDEX]);
                }
                if (AndNotZero(corner, LocationConfig.TR)) {
                    action.Invoke(splitedArray[TR_INDEX]);
                }
            }
        }

        // ==== Insert ====
        internal QuadTreeNode<T> Insert(T valuePtr, in Bounds2 bounds) {

            int nextDepth = depth + 1;

            var node = new QuadTreeNode<T>(treePtr, bounds, nextDepth);
            node.onlyID = Tree.GenOnlyID();
            node.SetAsLeaf(valuePtr);

            InsertNode(node, locationID, LocationConfig.NONE, depth);

            return node;

        }

        void InsertNode(QuadTreeNode<T> node, uint parentLocationID, byte cornerID, int parentDepth) {

            SetAsBranch();

            node.SetLocationID(GenLeafLocationID(parentLocationID, parentDepth, cornerID));
            node.SetDepth(depth + 1);

            // 层级已满时, 不再分割, 直接添加到 children
            if (depth >= Tree.MaxDepth || node.depth >= Tree.MaxDepth) {
                children.Add(node.onlyID, node);
                return;
            }

            if (!isSplit) {
                InsertNodeWhenNotSplit(node);
            } else {
                InsertNodeWhenSplit(node);
            }

        }

        void InsertNodeWhenSplit(QuadTreeNode<T> node) {

            var nodeBounds = node.bounds;

            var corner = splitedArray[BL_INDEX];
            if (corner.IsIntersectOrContains(nodeBounds)) {
                corner.InsertNode(node, node.locationID, LocationConfig.BL, corner.depth);
            }

            corner = splitedArray[BR_INDEX];
            if (corner.IsIntersectOrContains(nodeBounds)) {
                corner.InsertNode(node, node.locationID, LocationConfig.BR, corner.depth);
            }

            corner = splitedArray[TL_INDEX];
            if (corner.IsIntersectOrContains(nodeBounds)) {
                corner.InsertNode(node, node.locationID, LocationConfig.TL, corner.depth);
            }

            corner = splitedArray[TR_INDEX];
            if (corner.IsIntersectOrContains(nodeBounds)) {
                corner.InsertNode(node, node.locationID, LocationConfig.TR, corner.depth);
            }

        }

        void InsertNodeWhenNotSplit(QuadTreeNode<T> node) {

            // Children 小于 4 个时, 插入
            if (children.Count < 4) {
                children.Add(node.onlyID, node);
                return;
            }

            // Children 等于 4 个时, 首次分割
            if (children.Count == 4) {
                Split();
                InsertNodeWhenSplit(node);
            }

        }

        void Split() {

            int nextDepth = depth + 1;
            var size = bounds.Size * 0.5f;
            var halfSize = size * 0.5f;
            var center = bounds.Center;

            var blBounds = new Bounds2(center - halfSize, size);
            var brBounds = new Bounds2(new Vector2(center.x + halfSize.x, center.y - halfSize.y), size);
            var tlBounds = new Bounds2(new Vector2(center.x - halfSize.x, center.y + halfSize.y), size);
            var trBounds = new Bounds2(center + halfSize, size);

            var bl = new QuadTreeNode<T>(treePtr, blBounds, nextDepth);
            bl.SetLocationID(GenBranchLocationID(locationID, LocationConfig.BL, depth));
            bl.onlyID = Tree.GenOnlyID();
            splitedArray[BL_INDEX] = bl;

            var br = new QuadTreeNode<T>(treePtr, brBounds, nextDepth);
            br.SetLocationID(GenBranchLocationID(locationID, LocationConfig.BR, depth));
            br.onlyID = Tree.GenOnlyID();
            splitedArray[BR_INDEX] = br;

            var tl = new QuadTreeNode<T>(treePtr, tlBounds, nextDepth);
            tl.SetLocationID(GenBranchLocationID(locationID, LocationConfig.TL, depth));
            tl.onlyID = Tree.GenOnlyID();
            splitedArray[TL_INDEX] = tl;

            var tr = new QuadTreeNode<T>(treePtr, trBounds, nextDepth);
            tr.SetLocationID(GenBranchLocationID(locationID, LocationConfig.TR, depth));
            tr.onlyID = Tree.GenOnlyID();
            splitedArray[TR_INDEX] = tr;

            foreach (var kv in children) {
                var child = kv.Value;
                InsertNodeWhenSplit(child);
            }

            children.Clear();

            isSplit = true;

        }

        // ==== Remove ====
        internal void RemoveNode(ulong targetFullID) {

            byte targetCornerID = GetCornerIDFromFullID(targetFullID, depth + 1);
            if (targetCornerID > LocationConfig.FULL) {
                return;
            }

            uint targetOnlyID = (uint)(targetFullID >> 32);

            _ = children.Remove(targetOnlyID);

            VisitCorner(targetCornerID, corner => {
                if (corner != null) {
                    corner.RemoveNode(targetFullID);
                }
            });

        }

        // ==== Query ====
        internal void GetCandidateNodes(in Bounds2 bounds, HashSet<QuadTreeNode<T>> candidates) {

            if (IsLeaf()) {
                candidates.Add(this);
                return;
            } else {
                if (!IsIntersectOrContains(bounds)) {
                    return;
                }
            }

            if (isSplit) {
                for (int i = 0; i < splitedArray.Length; i += 1) {
                    var corner = splitedArray[i];
                    if (corner != null) {
                        corner.GetCandidateNodes(bounds, candidates);
                    }
                }
            }

            if (children.Count > 0) {
                foreach (var kv in children) {
                    var child = kv.Value;
                    child.GetCandidateNodes(bounds, candidates);
                }
            }

        }

        internal void GetCandidateValues(in Bounds2 bounds, HashSet<T> candidates) {

            if (IsLeaf()) {
                candidates.Add(Value);
                return;
            } else {
                if (!IsIntersectOrContains(bounds)) {
                    return;
                }
            }

            if (isSplit) {
                for (int i = 0; i < splitedArray.Length; i += 1) {
                    var corner = splitedArray[i];
                    if (corner != null) {
                        corner.GetCandidateValues(bounds, candidates);
                    }
                }
            }

            if (children.Count > 0) {
                foreach (var kv in children) {
                    var child = kv.Value;
                    child.GetCandidateValues(bounds, candidates);
                }
            }

        }

        // TODO: GetCandidates By FullID

    }

}