namespace GameArki.BTTreeNS {

    public static class BTTreeFactory {

        // ==== Container ====
        public static BTTreeNode CreateSelectorNode(IBTTreePrecondition precondition = null) {
            BTTreeNode node = new BTTreeNode(BTTreeNodeType.Selector, precondition, null);
            return node;
        }

        public static BTTreeNode CreateSequenceNode(IBTTreePrecondition precondition = null) {
            BTTreeNode node = new BTTreeNode(BTTreeNodeType.Sequence, precondition, null);
            return node;
        }

        public static BTTreeNode CreateParallelAndNode(IBTTreePrecondition precondition = null) {
            BTTreeNode node = new BTTreeNode(BTTreeNodeType.ParallelAnd, precondition, null);
            return node;
        }

        public static BTTreeNode CreateParallelOrNode(IBTTreePrecondition precondition = null) {
            BTTreeNode node = new BTTreeNode(BTTreeNodeType.ParallelOr, precondition, null);
            return node;
        }

        // ==== Action ====
        public static BTTreeNode CreateActionNode(IBTTreeAction action, IBTTreePrecondition precondition = null) {
            if (action == null) {
                throw new System.Exception("Action Cant Be Null");
            }
            BTTreeNode node = new BTTreeNode(BTTreeNodeType.Action, precondition, action);
            return node;
        }

    }

}