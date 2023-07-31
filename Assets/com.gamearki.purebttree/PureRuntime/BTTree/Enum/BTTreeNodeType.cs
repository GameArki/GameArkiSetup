namespace GameArki.BTTreeNS {

    public enum BTTreeNodeType : byte {

        None,
        Selector,
        Sequence,
        ParallelAnd,
        ParallelOr,
        Action,

    }

}