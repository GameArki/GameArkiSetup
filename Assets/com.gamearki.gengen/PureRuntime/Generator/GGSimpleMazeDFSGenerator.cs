using System;
using System.Collections.Generic;

namespace GameArki.GenGen {

    // Simple Maze (DFS)
    // From BottomLeft(0, 0) To TopRight(width, height)
    // 无环迷宫
    public class GGSimpleMazeDFSGenerator {

        Random rd;

        // -1 : 边界
        // 0  : 墙
        // 1  : 路
        public const int NODE_BORDER = -1;
        public const int NODE_WALL = 0;
        public const int NODE_ROAD = 1;

        // ==== Start ====
        int[] map;
        int maxCountExceptWall;

        Vec2Int size;
        public Vec2Int Size => size;

        // ==== Process ====
        int visitedCount;
        Stack<Vec2Int> visitedIndexStack;

        // ==== Result ====
        bool isDone;
        public bool IsDone => isDone;

        // ==== Temp ====
        Vec2Int curPos;
        public Vec2Int CurPos => curPos;

        List<Vec2Int> tmpDirList;

        public GGSimpleMazeDFSGenerator() { }

        public void Input(int width, int height, int startX, int startY) {

            this.map = new int[width * height];
            this.size = new Vec2Int() { x = width, y = height };
            this.rd = new Random();

            this.visitedCount = 0;
            if (width % 2 != 0) {
                width += 1;
            }
            if (height % 2 != 0) {
                height += 1;
            }
            this.maxCountExceptWall = width * height / 4;

            this.visitedIndexStack = new Stack<Vec2Int>();

            this.isDone = false;

            this.tmpDirList = new List<Vec2Int>(4);

            GenFirstStep(startX, startY);

        }

        void GenFirstStep(int x, int y) {
            Vec2Int startPos = new Vec2Int(x, y);
            int index = startPos.ToArrayIndex(size.x);
            map[index] = NODE_ROAD;
            visitedIndexStack.Push(startPos);
            visitedCount += 1;
            curPos = startPos;
        }

        // 一次性生成
        public void GenInstant() {
            while (!isDone) {
                GenByStep();
            }
        }

        public void GenByStep() {

            if (isDone) {
                return;
            }

            // 1. 随机选择一个方向
            // 2. 如果方向上未访问过，则标记为路，然后继续往前走
            // 3. 如果方向上已经访问过，则选择另一个方向
            // 4. 如果所有方向都已经访问过，则回退一步

            var dirList = tmpDirList;
            dirList.Clear();
            Vec2Int up = new Vec2Int(curPos.x, curPos.y + 2);
            Vec2Int down = new Vec2Int(curPos.x, curPos.y - 2);
            Vec2Int left = new Vec2Int(curPos.x - 2, curPos.y);
            Vec2Int right = new Vec2Int(curPos.x + 2, curPos.y);
            if (GetDirValue(up) == NODE_WALL) {
                dirList.Add(up);
            }
            if (GetDirValue(down) == NODE_WALL) {
                dirList.Add(down);
            }
            if (GetDirValue(left) == NODE_WALL) {
                dirList.Add(left);
            }
            if (GetDirValue(right) == NODE_WALL) {
                dirList.Add(right);
            }

            // shuffle
            for (int i = 0; i < dirList.Count; i += 1) {
                int index = rd.Next(0, dirList.Count);
                var tmp = dirList[i];
                dirList[i] = dirList[index];
                dirList[index] = tmp;
            }

            var visited = visitedIndexStack;
            if (dirList.Count == 0) {
                if (IsAllVisited() || visited.Count == 0) {
                    isDone = true;
                    visited.Clear();
                    return;
                }
                // 4. 如果所有方向都已经访问过，则回退一步
                _ = visited.Pop();
                curPos = visited.Peek();
                return;
            } else {
                // 1. 随机选择一个方向
                var nextPos = dirList[0];
                var arr = map;
                // 2. 方向上未访问过，标记为路
                // 3. 经过的中间点标记为路
                var diff = nextPos - curPos;
                var passPos = curPos + diff / 2;
                try {
                    arr[nextPos.ToArrayIndex(size.x)] = NODE_ROAD;
                    arr[passPos.ToArrayIndex(size.x)] = NODE_ROAD;
                } catch {
                    string err = $"nextPos: {nextPos}, curPos: {curPos}, passPos: {passPos}, size: {size}";
                    throw new Exception(err);
                }
                visited.Push(nextPos);
                curPos = nextPos;
                visitedCount += 1;
            }

        }

        bool IsAllVisited() {
            return visitedCount >= maxCountExceptWall;
        }

        int GetDirValue(Vec2Int pos) {
            if (pos.x < 0 || pos.x >= size.x || pos.y < 0 || pos.y >= size.y) {
                return NODE_BORDER;
            }
            return map[pos.ToArrayIndex(size.x)];
        }

        public ReadOnlySpan<int> GetMap() {
            return map;
        }

    }

}