using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameArki.BTTreeNS;

namespace GameArki.SampleApp {

    public class BTTreeDoNothingAction : IBTTreeAction {

        public BTTreeDoNothingAction() { }

        public void Inject() {

        }

        public void Enter() {

        }

        public bool Execute() {
            return false;
        }

        public void Exit() {

        }

    }

    public class BTTreeDontJudgePrecondition : IBTTreePrecondition {

        public void Inject() {

        }

        public bool CanEnter() {
            return true;
        }

    }

    public class BTTreeSampleApp : MonoBehaviour {

        BTTree tree;

        void Awake() {

            tree = new BTTree();

            // Selector: 子节点 选一个执行; 这个节点执行完 结束
            // Sequence: 子节点 顺序执行; 所有子节点执行完 结束
            // Parallel And: 子节点 并行执行; 所有节点都处于执行完 结束
            // Parallel Or: 子节点 并行执行; 其中一个节执行完 结束
            // Action: 执行节点

            BTTreeNode root = BTTreeFactory.CreateSelectorNode();
            BTTreeNode action = BTTreeFactory.CreateActionNode(new BTTreeDoNothingAction());
            root.AddChild(action);

            tree.Initialize(root);

        }

        void Update() {

            tree.Tick();

        }

    }
}