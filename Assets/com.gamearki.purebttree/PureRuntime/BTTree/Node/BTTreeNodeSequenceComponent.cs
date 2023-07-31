using System.Collections.Generic;

namespace GameArki.BTTreeNS {

    internal class BTTreeNodeSequenceComponent {

        int activeIndex;
        int nextIndex;

        internal BTTreeNodeSequenceComponent() { }

        internal bool Evaluate(List<BTTreeNode> children) {

            // 准备进入下一个
            if (activeIndex >= children.Count) {
                Reset(children);
                return false;
            }

            if (activeIndex == nextIndex) {
                var next = children[nextIndex];
                if (next.CanEnter()) {
                    nextIndex += 1;
                    return true;
                }
            }

            // 执行当前的
            var activeChild = children[activeIndex];
            bool res = activeChild.Evaluate();
            if (res) {
                return true;
            } else {
                if (nextIndex >= children.Count) {
                    // 整个 Sequence 执行结束
                    Reset(children);
                    return false;
                } else {
                    // 还有待执行的
                    activeIndex = nextIndex;
                    return true;
                }
            }

        }

        internal void TickSequence(List<BTTreeNode> children) {
            if (activeIndex >= children.Count) {
                return;
            }
            var activeChild = children[activeIndex];
            activeChild.Tick();
        }

        internal void Reset(List<BTTreeNode> children) {
            children.ForEach(value => value.Reset());
            activeIndex = 0;
            nextIndex = 0;
        }

    }
}