using System;
using System.Collections.Generic;
using GameArki.PathFinding.Generic;
// using static UnityEngine.Debug;

namespace GameArki.PathFinding.AStar {

    public class AstarEntity {

        int width;
        public int Width => width;

        public int length;
        public int Length => length;

        int[,] heightMap;
        int capacity;

        static readonly int MOVE_DIAGONAL_COST = 141;
        static readonly int MOVE_STRAIGHT_COST = 100;

        Heap<AStarNode> openList;
        Dictionary<long, AStarNode> openDic;
        Dictionary<long, bool> closedDic;
        ObjectPool<AStarNode> nodePool;

        List<Int2> path;
        List<Int2> smoothPath;

        public AstarEntity(int width, int length) {
            this.width = width;
            this.length = length;
            this.capacity = width * length;

            // 数据缓存
            heightMap = new int[width, length];

            openList = new Heap<AStarNode>(capacity);
            closedDic = new Dictionary<long, bool>();
            openDic = new Dictionary<long, AStarNode>();

            nodePool = new ObjectPool<AStarNode>(capacity);

            path = new List<Int2>(capacity);
            smoothPath = new List<Int2>(capacity);
        }

        public List<Int2> FindSmoothPath(in Int2 startPos, in Int2 endPos, in Int2 walkableHeightDiffRange, bool allowDiagonalMove) {
            FindSmoothPath(startPos, endPos, walkableHeightDiffRange, allowDiagonalMove, out var count);
            return smoothPath;
        }

        public List<Int2> FindSmoothPath(in Int2 startPos, in Int2 endPos, in Int2 walkableHeightDiffRange, bool allowDiagonalMove, out int count) {
            FindPath(startPos, endPos, walkableHeightDiffRange, allowDiagonalMove, out count);
            FindSmoothPath(path, walkableHeightDiffRange);
            return smoothPath;
        }

        public List<Int2> FindPath(in Int2 startPos, in Int2 endPos, in Int2 walkableHeightDiffRange, bool allowDiagonalMove) {
            FindPath(startPos, endPos, walkableHeightDiffRange, allowDiagonalMove, out var count);
            return path;
        }

        public List<Int2> FindPath(in Int2 startPos, in Int2 endPos, in Int2 walkableHeightDiffRange, bool allowDiagonalMove, out int calculateCount) {
            calculateCount = 0;

            if (CanGoStraight(startPos, endPos, walkableHeightDiffRange)) {
                path.Clear();
                path.Add(startPos);
                path.Add(endPos);
                return path;
            }

            closedDic.Clear();
            openDic.Clear();
            openList.Clear();
            Span<AStarNode> recycleNodes = new AStarNode[capacity];
            int recycleNodeCount = 0;

            // 初始化起点
            // - Get From Pool
            if (!nodePool.TryDequeue(out var startNode)) startNode = new AStarNode();
            recycleNodes[recycleNodeCount++] = startNode;
            startNode.pos = startPos;
            startNode.g = 0;
            startNode.h = GetManhattanDistance(startPos, endPos);
            startNode.f = 0;

            // 初始化终点
            // - Get From Pool
            if (!nodePool.TryDequeue(out var endNode)) endNode = new AStarNode();
            recycleNodes[recycleNodeCount++] = endNode;
            endNode.pos = endPos;

            if (!IsInBoundary(startNode.pos)) {
                return null;
            }
            if (!IsInBoundary(endNode.pos)) {
                return null;
            }

            // 将起点添加到开启列表中
            openList.Push(startNode);
            AStarNode currentNode = startNode;

            while (openList.Count > 0) {
                calculateCount++;
                // 找到开启列表中F值
                currentNode = GetLowestFNode(openList, endNode);
                // Log($"openList.Pop {currentNode.pos} F  {currentNode.f}");
                var curNodePos = currentNode.pos;
                var endNodePos = endNode.pos;

                // 从开启列表中移除当前节点，并将其添加到关闭列表中
                var posKey = CombineKey(curNodePos.X, curNodePos.Y);
                closedDic.Add(posKey, true);

                // 如果当前节点为终点，则找到了最短路径
                if (curNodePos.ValueEquals(endNodePos)) {
                    // 使用栈来保存路径
                    Stack<AStarNode> pathStack = new Stack<AStarNode>();
                    while (currentNode != null) {
                        pathStack.Push(currentNode);
                        currentNode = currentNode.parent;
                    }
                    path.Clear();
                    while (pathStack.TryPop(out var node)) {
                        path.Add(node.pos);
                    }

                    RecycleToNodePool(recycleNodes, recycleNodeCount);
                    return path;
                }

                SearchAndUpdateNeighbourhood(recycleNodes, ref recycleNodeCount, walkableHeightDiffRange, allowDiagonalMove, endNode, currentNode);

            }

            // 如果开启列表为空，则无法找到路径
            RecycleToNodePool(recycleNodes, recycleNodeCount);
            path.Clear();
            return path;

        }

        void RecycleToNodePool(Span<AStarNode> recycleNodes, int recycleNodeCount) {
            for (int i = 0; i < recycleNodeCount; i++) {
                var node = recycleNodes[i];
                node.Clear();
                nodePool.Enqueue(node);
            }
        }

        void SearchAndUpdateNeighbourhood(Span<AStarNode> recycleNodes, ref int recycleNodeCount, in Int2 walkableHeightDiffRange, bool allowDiagonalMove, AStarNode endNode, AStarNode currentNode) {
            // 获取当前节点的周围节点
            // 获取当前节点的位置
            var currentPos = currentNode.pos;
            int x = currentPos.X;
            int y = currentPos.Y;
            // 获取四周的节点
            Int2 topPos = new Int2(x, y + 1);
            SearchAndUpdateNeighbour(
                currentNode: currentNode,
                endNode: endNode,
                allowDiagonalMove: allowDiagonalMove,
                walkableHeightDiffRange: walkableHeightDiffRange,
                pos: topPos,
                recycleNodes: recycleNodes,
                recycleNodeCount: ref recycleNodeCount
            );
            Int2 bottomPos = new Int2(x, y - 1);
            SearchAndUpdateNeighbour(
                currentNode: currentNode,
                endNode: endNode,
                allowDiagonalMove: allowDiagonalMove,
                walkableHeightDiffRange: walkableHeightDiffRange,
                pos: bottomPos,
                recycleNodes: recycleNodes,
                recycleNodeCount: ref recycleNodeCount
            );
            Int2 leftPos = new Int2(x - 1, y);
            SearchAndUpdateNeighbour(
                currentNode: currentNode,
                endNode: endNode,
                allowDiagonalMove: allowDiagonalMove,
                walkableHeightDiffRange: walkableHeightDiffRange,
                pos: leftPos,
                recycleNodes: recycleNodes,
                recycleNodeCount: ref recycleNodeCount
            );
            Int2 rightPos = new Int2(x + 1, y);
            SearchAndUpdateNeighbour(
                currentNode: currentNode,
                endNode: endNode,
                allowDiagonalMove: allowDiagonalMove,
                walkableHeightDiffRange: walkableHeightDiffRange,
                pos: rightPos,
                recycleNodes: recycleNodes,
                recycleNodeCount: ref recycleNodeCount
            );

            if (allowDiagonalMove) {
                Int2 top_leftPos = new Int2(x - 1, y + 1);
                SearchAndUpdateNeighbour(
                    currentNode: currentNode,
                    endNode: endNode,
                    allowDiagonalMove: allowDiagonalMove,
                    walkableHeightDiffRange: walkableHeightDiffRange,
                    pos: top_leftPos,
                    recycleNodes: recycleNodes,
                    recycleNodeCount: ref recycleNodeCount
                );
                Int2 bottom_leftPos = new Int2(x - 1, y - 1);
                SearchAndUpdateNeighbour(
                    currentNode: currentNode,
                    endNode: endNode,
                    allowDiagonalMove: allowDiagonalMove,
                    walkableHeightDiffRange: walkableHeightDiffRange,
                    pos: bottom_leftPos,
                    recycleNodes: recycleNodes,
                    recycleNodeCount: ref recycleNodeCount
                );
                Int2 top_rightPos = new Int2(x + 1, y + 1);
                SearchAndUpdateNeighbour(
                    currentNode: currentNode,
                    endNode: endNode,
                    allowDiagonalMove: allowDiagonalMove,
                    walkableHeightDiffRange: walkableHeightDiffRange,
                    pos: top_rightPos,
                    recycleNodes: recycleNodes,
                    recycleNodeCount: ref recycleNodeCount
                );
                Int2 bottom_rightPos = new Int2(x + 1, y - 1);
                SearchAndUpdateNeighbour(
                    currentNode: currentNode,
                    endNode: endNode,
                    allowDiagonalMove: allowDiagonalMove,
                    walkableHeightDiffRange: walkableHeightDiffRange,
                    pos: bottom_rightPos,
                    recycleNodes: recycleNodes,
                    recycleNodeCount: ref recycleNodeCount
                );
            }

        }

        void SearchAndUpdateNeighbour(AStarNode currentNode, AStarNode endNode, bool allowDiagonalMove,
        in Int2 pos, in Int2 walkableHeightDiffRange,
        Span<AStarNode> recycleNodes, ref int recycleNodeCount
        ) {
            // - 邻接点是否可走
            if (!IsWalkableNeighbour(pos, currentNode.pos, walkableHeightDiffRange)) return;

            // - 是否是新的邻接点,是则加入openDic以及openList
            var posKey = CombineKey(pos.X, pos.Y);
            bool gotFromOpenDic = openDic.TryGetValue(posKey, out var neighbourNode);
            bool gotFromPool = !gotFromOpenDic ? nodePool.TryDequeue(out neighbourNode) : false;
            bool needCreate = !gotFromOpenDic && !gotFromPool;
            // if (gotFromPool) Log($"nodePool Dequeue: {neighbourNode.pos}");
            if (needCreate) neighbourNode = new AStarNode();
            if (needCreate || gotFromPool) recycleNodes[recycleNodeCount++] = neighbourNode;
            neighbourNode.pos = pos;

            // - 经过邻接点就需要重新计算GHF
            var g_offset = GetDistance(currentNode, neighbourNode, allowDiagonalMove);
            int newG = currentNode.g + g_offset;

            // 如果新的G值比原来的G值小,计算新的F值 
            if (!gotFromOpenDic || newG < neighbourNode.g) {
                neighbourNode.g = newG;
                neighbourNode.h = GetDistance(neighbourNode, endNode, allowDiagonalMove);
                neighbourNode.f = neighbourNode.g + neighbourNode.h;
                neighbourNode.parent = currentNode;
            }

            if (!gotFromOpenDic) {
                openList.Push(neighbourNode);
                // Log($"openList.Push {neighbourNode.pos} F  {neighbourNode.f} newG:{newG}");
                openDic.Add(posKey, neighbourNode);
            }
        }

        public void SetXYHeight(in Int2 pos, int height) {
            heightMap[pos.X, pos.Y] = height;
        }

        public int GetXYHeight(in Int2 pos) {
            return heightMap[pos.X, pos.Y];
        }

        AStarNode GetLowestFNode(Heap<AStarNode> openList, AStarNode endNode) {
            return openList.Pop();
        }

        int GetDistance(AStarNode node1, AStarNode node2, bool allowDiagonalMove) {
            var pos1 = node1.pos;
            var pos2 = node2.pos;
            if (allowDiagonalMove) {
                int xDistance = Math.Abs(pos1.X - pos2.X);
                int yDistance = Math.Abs(pos1.Y - pos2.Y);
                int remaining = Math.Abs(xDistance - yDistance);
                return MOVE_DIAGONAL_COST * Math.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
            } else {
                return GetManhattanDistance(pos1, pos2);
            }
        }

        int GetManhattanDistance(in Int2 pos1, in Int2 pos2) {
            return Math.Abs(pos1.X - pos2.X) + Math.Abs(pos1.Y - pos2.Y);
        }

        bool IsWalkableNeighbour(in Int2 neighbourPos, in Int2 curPos, in Int2 walkableHeightDiffRange) {
            if (!IsCanReach(neighbourPos, curPos, walkableHeightDiffRange)) return false;
            if (closedDic.TryGetValue(CombineKey(neighbourPos.X,neighbourPos.Y), out var flag) && flag) return false;
            return true;
        }

        bool IsInBoundary(in Int2 pos) {
            var x = pos.X;
            var y = pos.Y;
            if (x >= width || x < 0 || y >= length || y < 0) {
                return false;
            }

            return true;
        }

        bool IsWalkable(in Int2 tarPos, in Int2 fromPos, in Int2 walkableHeightDiffRange) {
            var hDiff = heightMap[tarPos.X, tarPos.Y] - heightMap[fromPos.X, fromPos.Y];
            return hDiff <= walkableHeightDiffRange.Y && hDiff >= walkableHeightDiffRange.X;
        }

        bool IsCanReach(in Int2 tarPos, in Int2 fromPos, in Int2 walkableHeightDiffRange) {
            return IsInBoundary(tarPos) && IsWalkable(tarPos, fromPos, walkableHeightDiffRange);
        }

        long CombineKey(int x, int y) {
            long key = (long)x << 32;
            key |= (long)y;
            return key;
        }

        #region [路径点优化]

        public List<Int2> FindSmoothPath(List<Int2> path, in Int2 walkableHeightDiffRange) {
            if (path.Count == 0) {
                smoothPath.Clear();
                return smoothPath;
            }

            var pos1 = path[0];
            var pos2 = path[1];
            int pos1Index = 0;

            smoothPath.Clear();
            smoothPath.Add(pos1);
            smoothPath.Add(pos2);
            for (int i = 2; i < path.Count; i++) {
                var curPos1 = path[i - 1];
                var curPos2 = path[i];
                if (IsSlopeEqual(pos1, pos2, curPos1, curPos2)) {
                    // 斜率相同为同一条直线路径上,则可去除期间多余路径点
                    // 更新节点
                    pos2 = curPos2;
                    smoothPath.RemoveAt(smoothPath.Count - 1);
                    smoothPath.Add(curPos2);
                } else {
                    // 斜率不相同且可以直达,则可去除期间多余路径点
                    while (!CanGoStraight(pos1, curPos2, walkableHeightDiffRange)) {
                        // UnityEngine.Debug.Log($" pos1{pos1}  curPos2{curPos2}  Cant GoStraight");
                        var nextPos = path[pos1Index + 1];
                        if (nextPos == curPos1) break;

                        pos1 = nextPos;
                        pos1Index++;
                    }

                    pos2 = curPos2;
                    smoothPath.ForEach((p) => {
                        // UnityEngine.Debug.Log($"  p {p}");
                    });
                    smoothPath.RemoveAt(smoothPath.Count - 1);
                    smoothPath.Add(pos1);
                    smoothPath.Add(curPos2);
                    // UnityEngine.Debug.Log($" pos1{pos1}  curPos2{curPos2}  GoStraight pos2:{pos2}   curPos1:{curPos1} ");
                }
            }

            return smoothPath;
        }

        bool CanGoStraight(in Int2 startPos, in Int2 endPos, in Int2 walkableHeightDiffRange) {
            bool flag;
            bool flag1;
            bool flag2;
            int k_son = startPos.Y - endPos.Y;
            int k_mom = startPos.X - endPos.X;
            bool isPositive = (k_son > 0 && k_mom > 0) || (k_son < 0 && k_mom < 0);

            flag = IsP2PWalkable(startPos, endPos, startPos, walkableHeightDiffRange);
            if (isPositive) {
                Int2 p1 = new Int2();
                p1.X = startPos.X;
                p1.Y = startPos.Y + 1;
                Int2 p2 = new Int2();
                p2.X = endPos.X;
                p2.Y = endPos.Y + 1;

                flag1 = IsP2PWalkable(p1, p2, startPos, walkableHeightDiffRange);
                Int2 p3 = new Int2(startPos.X + 1, startPos.Y);
                Int2 p4 = new Int2(endPos.X + 1, endPos.Y);
                flag2 = IsP2PWalkable(p3, p4, startPos, walkableHeightDiffRange);
                return flag && flag1 && flag2;
            } else {
                Int2 p1 = new Int2(startPos.X + 1, startPos.Y + 1);
                Int2 p2 = new Int2(endPos.X + 1, endPos.Y + 1);
                flag1 = IsP2PWalkable(p1, p2, startPos, walkableHeightDiffRange);
                Int2 p3 = new Int2(startPos.X + 1, startPos.Y);
                Int2 p4 = new Int2(endPos.X + 1, endPos.Y);
                flag2 = IsP2PWalkable(p3, p4, startPos, walkableHeightDiffRange);
                return flag && flag1 && flag2;
            }

        }

        bool IsSlopeEqual(Int2 pos1, Int2 pos2, Int2 pos3, Int2 pos4) {
            int k1_son = (pos2.Y - pos1.Y);
            int k1_mom = (pos2.X - pos1.X);
            int k2_son = (pos4.Y - pos3.Y);
            int k2_mom = (pos4.X - pos3.X);
            if (k1_son == 0 && k1_mom == 0) return false;
            if (k2_son == 0 && k2_mom == 0) return false;
            return k1_son * k2_mom == k2_son * k1_mom;
        }

        bool IsP2PWalkable(Int2 p1, Int2 p2, Int2 startPos, in Int2 walkableHeightDiffRange) {
            if (p1.ValueEquals(p2)) return true;

            // 保证顺序
            if (p1.X > p2.X) {
                Int2 tempPos = p1;
                p1 = p2;
                p2 = tempPos;
            }
            if (p1.X == p2.X && p1.Y > p2.Y) {
                Int2 tempPos = p1;
                p1 = p2;
                p2 = tempPos;
            }

            int x1, y1, x2, y2, A, B;
            x1 = p1.X;
            y1 = p1.Y;
            x2 = p2.X;
            y2 = p2.Y;

            Int2 currentPos = p1;
            bool isXSame = x1 == x2;
            bool isYSame = y1 == y2;
            if (isXSame) {
                while (!currentPos.ValueEquals(p2)) {
                    var fromPos = currentPos;
                    currentPos += new Int2(0, 1);
                    if (!IsCanReach(currentPos, fromPos, walkableHeightDiffRange)) return false;
                    currentPos += new Int2(0, 1);
                }
            } else if (isYSame) {
                while (!currentPos.ValueEquals(p2)) {
                    var fromPos = currentPos;
                    currentPos += new Int2(1, 0);
                    if (!IsCanReach(currentPos, fromPos, walkableHeightDiffRange)) return false;
                    currentPos += new Int2(1, 0);
                }
            } else {
                A = y1 - y2;
                B = x2 - x1;
                int d = 0;
                int d1 = 0;
                int d2 = 0;
                Int2 posAdd1 = new Int2();
                Int2 posAdd2 = new Int2();
                var a_abs = Math.Abs(A);
                var b_abs = Math.Abs(B);
                bool isSlopeABSBiggerThanOne = a_abs > b_abs;
                bool isSlopeBiggerThanZero = (A > 0 && -B > 0) || (A < 0 && -B < 0);

                var fromPos = startPos;
                //斜率为正，则不移动，斜率为负，则向下移动一个位置
                Int2 offset = new Int2(0, isSlopeBiggerThanZero ? 0 : -1);
                currentPos += offset;
                p2 += offset;
                if (!IsCanReach(currentPos, fromPos, walkableHeightDiffRange)) {
                    return false;
                }

                // 计算d d1 d2
                if (isSlopeBiggerThanZero) {
                    d = A + B;
                    d2 = A + B;
                } else {
                    d = A - B;
                    d2 = A - B;
                }
                d1 = A;
                if (isSlopeABSBiggerThanOne) d1 = isSlopeBiggerThanZero ? B : -B;
                d = isSlopeBiggerThanZero ? (A + B) : A - B;
                d2 = d;

                // 计算 posAdd1 posAdd2
                if (!isSlopeABSBiggerThanOne) {
                    posAdd1 = new Int2(1, 0);
                    posAdd2 = new Int2(1, isSlopeBiggerThanZero ? 1 : -1);
                } else {
                    //斜率大于1
                    posAdd1 = new Int2(0, isSlopeBiggerThanZero ? 1 : -1);
                    posAdd2 = new Int2(1, isSlopeBiggerThanZero ? 1 : -1);
                }

                // 遍历路经过的点
                while (!currentPos.ValueEquals(p2)) {
                    bool isXYBothChange;
                    if (!isSlopeABSBiggerThanOne) {
                        isXYBothChange = (isSlopeBiggerThanZero && d <= 0) || (!isSlopeBiggerThanZero && d >= 0);
                    } else {
                        //斜率大于1
                        isXYBothChange = (isSlopeBiggerThanZero && d >= 0) || (!isSlopeBiggerThanZero && d <= 0);
                    }

                    if (isXYBothChange) {
                        currentPos += posAdd2;
                        d += d2;
                    } else {
                        currentPos += posAdd1;
                        d += d1;
                    }

                    if (currentPos.ValueEquals(p2)) {
                        return true;
                    }

                    if (!IsCanReach(currentPos, fromPos, walkableHeightDiffRange)) {
                        return false;
                    }

                }

                if (currentPos.Y != p2.Y) return false;
            }

            return true;
        }

        #endregion


    }

}