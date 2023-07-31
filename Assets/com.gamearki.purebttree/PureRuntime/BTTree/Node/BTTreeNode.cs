using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GameArki.BTTreeNS {

    public sealed class BTTreeNode {

        // if nodeType == Action
        //      no children
        // else
        //      no action
        BTTreeNodeType nodeType;

        IBTTreePrecondition precondition;

        List<BTTreeNode> children;

        BTTreeNodeSelectorComponent selectorComponent;
        BTTreeNodeSequenceComponent sequenceComponent;
        BTTreeNodeParallelComponent parallelComponent;
        BTTreeNodeActionComponent actionComponent;

        bool isActivated;
        public bool IsActivated => isActivated;

        internal BTTreeNode(BTTreeNodeType nodeType, IBTTreePrecondition precondition, IBTTreeAction action) {

            this.precondition = precondition;

            this.nodeType = nodeType;
            if (nodeType == BTTreeNodeType.Selector) {
                this.children = new List<BTTreeNode>();
                this.selectorComponent = new BTTreeNodeSelectorComponent();
            } else if (nodeType == BTTreeNodeType.Sequence) {
                this.children = new List<BTTreeNode>();
                this.sequenceComponent = new BTTreeNodeSequenceComponent();
            } else if (nodeType == BTTreeNodeType.ParallelAnd) {
                this.children = new List<BTTreeNode>();
                this.parallelComponent = new BTTreeNodeParallelComponent();
            } else if (nodeType == BTTreeNodeType.ParallelOr) {
                this.children = new List<BTTreeNode>();
                this.parallelComponent = new BTTreeNodeParallelComponent();
            } else if (nodeType == BTTreeNodeType.Action) {
                this.children = new List<BTTreeNode>(0);
                this.actionComponent = new BTTreeNodeActionComponent(action);
            }
        }

        public void Activate() {
            this.isActivated = true;
            children?.ForEach(value => value.Activate());
        }

        public void Deactivate() {
            this.isActivated = false;
            children?.ForEach(value => value.Deactivate());
        }

        public void AddChild(BTTreeNode node) {
            if (nodeType == BTTreeNodeType.Action) {
                throw new System.Exception("Dont Add Child Into An Action");
            }
            children.Add(node);
            if (nodeType == BTTreeNodeType.ParallelOr || nodeType == BTTreeNodeType.ParallelAnd) {
                parallelComponent.AddRes();
            }
        }

        public void RemoveChild(BTTreeNode node) {
            int index = children.FindIndex(value => value == node);
            if (index != -1) {
                var child = children[index];
                if (nodeType == BTTreeNodeType.ParallelOr || nodeType == BTTreeNodeType.ParallelAnd) {
                    parallelComponent.RemoveResAt(index);
                } else if (nodeType == BTTreeNodeType.Selector) {
                    selectorComponent.Remove(child);
                }
                children.RemoveAt(index);
            }
        }

        internal bool CanEnter() {
            if (!isActivated) {
                return false;
            }
            if (precondition == null) {
                return true;
            }
            return precondition.CanEnter();
        }

        internal bool Evaluate() {

            if (!isActivated) {
                return false;
            }

            if (nodeType == BTTreeNodeType.Selector) {
                return selectorComponent.Evaluate(children);
            } else if (nodeType == BTTreeNodeType.Sequence) {
                return sequenceComponent.Evaluate(children);
            } else if (nodeType == BTTreeNodeType.Action) {
                return actionComponent.Evaluate();
            } else if (nodeType == BTTreeNodeType.ParallelAnd) {
                return parallelComponent.EvaluateAnd(children);
            } else if (nodeType == BTTreeNodeType.ParallelOr) {
                return parallelComponent.EvaluateOr(children);
            } else {
                throw new System.Exception("Node 节点类型不可为 None");
            }

        }

        internal void Tick() {

            if (!isActivated) {
                return;
            }

            if (nodeType == BTTreeNodeType.Selector) {
                selectorComponent.TickSelector();
            } else if (nodeType == BTTreeNodeType.Sequence) {
                sequenceComponent.TickSequence(children);
            } else if (nodeType == BTTreeNodeType.Action) {
                actionComponent.TickAction();
            } else if (nodeType == BTTreeNodeType.ParallelAnd || nodeType == BTTreeNodeType.ParallelOr) {
                parallelComponent.TickParallel(children);
            } else {
                throw new System.Exception("Node 节点类型不可为 None");
            }

        }

        public void Reset() {
            children.ForEach(value => value.Reset());
            selectorComponent?.Reset(children);
            sequenceComponent?.Reset(children);
            parallelComponent?.Reset(children);
            actionComponent?.Reset();
        }

        public string GetString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(nodeType.ToString());
            if (nodeType == BTTreeNodeType.Action) {
                sb.Append($": {actionComponent.GetString()}");
            }
            return sb.ToString();
        }

    }

}