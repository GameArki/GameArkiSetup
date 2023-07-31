using System.Collections.Generic;

namespace GameArki.BTTreeNS {

    internal class BTTreeNodeSelectorComponent {

        BTTreeNode activedChild;

        internal BTTreeNodeSelectorComponent() { }

        internal void Remove(BTTreeNode child) {
            if (activedChild == child) {
                activedChild = null;
            }
        }

        internal bool Evaluate(List<BTTreeNode> children) {

            if (activedChild == null) {
                for (int i = 0; i < children.Count; i += 1) {
                    var child = children[i];
                    if (child.CanEnter()) {
                        activedChild = child;
                        return true;
                    }
                }
                return false;
            } else {
                bool res = activedChild.Evaluate();
                if (res) {
                    return true;
                } else {
                    Reset(children);
                    return false;
                }
            }

        }

        internal void TickSelector() {
            if (activedChild != null) {
                activedChild.Tick();
            }
        }

        internal void Reset(List<BTTreeNode> children) {
            children.ForEach(value => value.Reset());
            activedChild = null;
        }

    }
}