using System;
using System.Collections.Generic;

namespace GameArki.GenGen {

    // Simple Maze Eller
    // BottomLeft To TopRight
    /*
        1. 从左到右，每个节点随机生成一个集合编号, 并打通与左边相同集合编号的节点
            [1, 1, 2, 3, 1, 4, 4, 4]
        如有下一行
            2a. 相同区域向下随机延伸至少一格
                [1, 1, 2, 3, 1, 4, 4, 4]
                [1, 0, 2, 3, 1, 4, 0, 0]
            3a. 未延伸的所有节点(0), 随机生成一个集合编号
                [1, 1, 2, 3, 1, 4, 4, 4]
                [1, 2, 2, 3, 1, 4, 1, 3]
            4a. 打通上下相同集合的墙, 打通左右相同集合的墙
        如无下一行
            2b. 打通左右所有墙
    */
    public class GGPerfectMazeEllerGenerator {

        public struct Node {
            int setIndex;
            public int SetIndex => setIndex;
            public void SetSetIndex(int value) => setIndex = value;

        }

        public const int NODE_WALL = 0;
        public const int NODE_ROAD = 1;

        int nextLineRate;

        Random rd;

        int[] map;
        int setTypeCount;

        int width;
        int height;
        public Vec2Int Size => new Vec2Int(width, height);

        enum Status { None, S1, S2, S3, S4, S5, Done }
        Status status;

        // ==== Temp ====
        Node[] line;
        Node[] nextLine;

        int[] borderCursorLine;
        int borderLineCount;
        int borderIndex;

        // - 代表在 Line 中的 Index
        int cursorXIndex;

        // - 代表访问到的 Map 中的 y
        int cursorY;

        // - 代表在 Map 中的位置, 不会访问到墙
        public Vec2Int Pos => new Vec2Int(cursorXIndex * 2, cursorY * 2);

        public GGPerfectMazeEllerGenerator() {
            this.status = Status.None;
        }

        public void Input(int width, int originalHeight, int setTypeCount, int seed = 0) {

            if (seed == 0) {
                this.rd = new Random();
            } else {
                this.rd = new Random(seed);
            }

            this.width = width;
            this.height = originalHeight;

            this.map = new int[width * originalHeight];

            this.setTypeCount = setTypeCount;

            nextLineRate = 50;

            this.line = new Node[width / 2 + width % 2];
            this.nextLine = new Node[width / 2 + width % 2];
            this.borderCursorLine = new int[width];

            S1_Enter();

        }

        public void GenInstant() {
            while (status != Status.Done) {
                GenByStep();
            }
        }

        public void GenByStep() {

            switch (status) {
                case Status.S1: S1_Exe(); break;
                case Status.S2: S2_Exe(); break;
                case Status.S3: S3_Exe(); break;
                case Status.S4: S4_Exe(); break;
                case Status.S5: S5_Exe(); break;
                default:
                    System.Console.WriteLine("GGPerfectMazeEllerGenerator: Error Status: " + status);
                    break;
            }

        }

        void S1_Enter() {
            System.Console.WriteLine("S1 Enter: " + cursorY + ", " + height);

            borderCursorLine[0] = 0;
            borderLineCount = 1;
            borderIndex = 0;

            for (int i = 0; i < line.Length; i += 1) {
                line[i] = default;
                nextLine[i] = default;
            }
            cursorXIndex = 0;

            status = Status.S1;
        }

        void S1_Exe() {

            // 当前行的节点
            Node node = new Node();
            node.SetSetIndex(CreateIndex());
            line[cursorXIndex] = node;

            // 地图设为路
            int idx = Pos.ToArrayIndex(width);
            System.Console.WriteLine(idx + "/" + map.Length);
            SetAsRoad(idx);

            // 找左边一格
            if (cursorXIndex > 0) {
                var leftNode = line[cursorXIndex - 1];

                // 如果左边一格的集合编号相同, 则打通
                if (leftNode.SetIndex == node.SetIndex) {
                    var leftPos = WallLeft();
                    SetAsRoad(leftPos.ToArrayIndex(width));
                } else {
                    // 否则, 记录为边界
                    // [1, 1, 2, 1, 3, 4]
                    //  ^     ^  ^  ^  ^
                    borderCursorLine[borderLineCount] = cursorXIndex;
                    borderLineCount += 1;
                }
            }

            if (cursorXIndex + 1 < line.Length) {
                cursorXIndex += 1;
            } else {
                if (IsLastLine()) {
                    S5_Enter();
                } else {
                    cursorY += 1;
                    S2_Enter();
                }
            }
        }

        // - S2a: line 向 nextLine 生成一个相同集合的节点
        // line [1, 1, 2, 2, 1, 3, 4, 4, 4, 4]
        // next [1, 0, 0, 2, 1, 3, 0, 0, 0, 4]
        void S2_Enter() {
            System.Console.WriteLine("S2 Enter: " + cursorY + ", " + height);
            cursorXIndex = 0;
            borderIndex = 0;
            borderCursorLine[borderLineCount] = line.Length;
            borderLineCount += 1;
            status = Status.S2;
        }

        void S2_Exe() {

            int leftIndex = borderCursorLine[borderIndex];
            int rightIndex = borderCursorLine[borderIndex + 1];

            int sameSetIndex = rd.Next(leftIndex, rightIndex);
            // System.Console.WriteLine($"RD: {sameSetIndex}, [{leftIndex}, {rightIndex}])");
            nextLine[sameSetIndex] = line[sameSetIndex];

            if (borderIndex + 2 < borderLineCount) {
                borderIndex += 1;
                cursorXIndex = sameSetIndex;
            } else {
                S3_Enter();
            }
        }

        // - S3a: nextLine 剩余空位随机生成
        void S3_Enter() {
            System.Console.WriteLine("S3 Enter: " + cursorY + ", " + height);
            cursorXIndex = 0;
            borderLineCount = 0;
            borderIndex = 0;
            status = Status.S3;
        }

        void S3_Exe() {

            var node = line[cursorXIndex];
            var nextNode = nextLine[cursorXIndex];
            if (nextNode.SetIndex != node.SetIndex) {
                int rate = rd.Next(0, 100);
                if (rate < nextLineRate) {
                    nextNode.SetSetIndex(node.SetIndex);
                } else {
                    nextNode.SetSetIndex(CreateIndex());
                }
            }
            nextLine[cursorXIndex] = nextNode;

            if (cursorXIndex + 1 < line.Length) {
                cursorXIndex += 1;
            } else {
                S4_Enter();
            }

        }

        void S4_Enter() {
            System.Console.WriteLine("S4 Enter: " + cursorY + ", " + height);
            cursorXIndex = 0;
            status = Status.S4;
        }

        void S4_Exe() {

            var node = line[cursorXIndex];
            var nextNode = nextLine[cursorXIndex];

            // 打通上下
            if (node.SetIndex == nextNode.SetIndex) {
                var verticalWallPos = new Vec2Int(Pos.x, Pos.y - 1);
                int idx = verticalWallPos.ToArrayIndex(width);
                if (idx < map.Length) {
                    SetAsRoad(idx);
                }
            }

            // 打通左右
            // if (cursorXIndex - 1 >= 0) {
            //     var nextLeftNode = nextLine[cursorXIndex - 1];
            //     if (nextLeftNode.SetIndex == nextNode.SetIndex) {
            //         var horizontalWallPos = new Vec2Int(Pos.x - 1, Pos.y);
            //         SetAsRoad(horizontalWallPos.ToArrayIndex(width));
            //     }
            // }

            if (cursorXIndex + 1 >= line.Length) {
                if (IsLastLine()) {
                    S5_Enter();
                } else {
                    S1_Enter();
                }
            } else {
                cursorXIndex += 1;
            }
        }

        void S5_Enter() {
            cursorY = height / 2 + height % 2 - 1;
            cursorXIndex = 0;
            status = Status.S5;
        }

        void S5_Exe() {
            var node = line[cursorXIndex];
            if (cursorXIndex + 1 < line.Length) {
                var horizontalWallPos = new Vec2Int(Pos.x + 1, Pos.y);
                SetAsRoad(horizontalWallPos.ToArrayIndex(width));
            }

            if (cursorXIndex + 1 >= line.Length) {
                cursorXIndex = 0;
                status = Status.Done;
            } else {
                cursorXIndex += 1;
            }
        }

        Vec2Int WallLeft() {
            return new Vec2Int(Pos.x - 1, Pos.y);
        }

        Vec2Int WallRight() {
            return new Vec2Int(Pos.x + 1, Pos.y);
        }

        Vec2Int WallUp() {
            return new Vec2Int(Pos.x, Pos.y - 1);
        }

        Vec2Int WallDown() {
            return new Vec2Int(Pos.x, Pos.y + 1);
        }

        void SetAsRoad(int index) {
            map[index] = NODE_ROAD;
        }

        int CreateIndex() {
            return rd.Next(1, setTypeCount + 1);
        }

        bool IsLastLine() {
            return cursorY >= height / 2 + height % 2;
        }

        // ==== External ====
        public ReadOnlySpan<int> GetMap() {
            return map;
        }

        public Node[] GetLine() {
            return line;
        }

        public Node[] GetNextLine() {
            return nextLine;
        }

    }

}