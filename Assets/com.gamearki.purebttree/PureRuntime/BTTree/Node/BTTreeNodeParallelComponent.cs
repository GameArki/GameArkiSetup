using System.Collections.Generic;

namespace GameArki.BTTreeNS {

    internal class BTTreeNodeParallelComponent {

        enum BTTreeNodeResult : byte {
            NodeReady,
            NodeRunning,
            NodeEnd,
        }

        List<BTTreeNodeResult> resList;

        internal BTTreeNodeParallelComponent() {
            this.resList = new List<BTTreeNodeResult>();
        }

        internal void AddRes() {
            resList.Add(BTTreeNodeResult.NodeReady);
        }

        internal void RemoveResAt(int index) {
            resList.RemoveAt(index);
        }

        internal bool EvaluateAnd(List<BTTreeNode> children) {
            int endCount = 0;
            for (int i = 0; i < children.Count; i += 1) {
                var child = children[i];
                var res = resList[i];
                if (res == BTTreeNodeResult.NodeReady) {
                    bool can = child.CanEnter();
                    if (can) {
                        resList[i] = BTTreeNodeResult.NodeRunning;
                    }
                } else if (res == BTTreeNodeResult.NodeRunning) {
                    bool ev = child.Evaluate();
                    if (!ev) {
                        resList[i] = BTTreeNodeResult.NodeEnd;
                        endCount += 1;
                    }
                } else if (res == BTTreeNodeResult.NodeEnd) {
                    endCount += 1;
                }
            }

            if (endCount >= children.Count) {
                Reset(children);
                return false;
            } else {
                return true;
            }
        }

        internal bool EvaluateOr(List<BTTreeNode> children) {

            int endCount = 0;
            for (int i = 0; i < children.Count; i += 1) {
                var child = children[i];
                var res = resList[i];
                if (res == BTTreeNodeResult.NodeReady) {
                    bool can = child.CanEnter();
                    if (can) {
                        resList[i] = BTTreeNodeResult.NodeRunning;
                    }
                } else if (res == BTTreeNodeResult.NodeRunning) {
                    bool ev = child.Evaluate();
                    if (!ev) {
                        endCount += 1;
                        break;
                    }
                }
            }

            if (endCount > 0) {
                Reset(children);
                return false;
            } else {
                return true;
            }
        }

        internal void TickParallel(List<BTTreeNode> children) {
            for (int i = 0; i < children.Count; i += 1) {
                var res = resList[i];
                if (res == BTTreeNodeResult.NodeRunning) {
                    var child = children[i];
                    child.Tick();
                }
            }
        }

        internal void Reset(List<BTTreeNode> children) {
            for (int i = 0; i < resList.Count; i += 1) {
                resList[i] = BTTreeNodeResult.NodeReady;
            }
            children.ForEach(value => value.Reset());
        }

    }
}