namespace GameArki.Anymotion {

    // 向相同 State 过渡时的策略
    public enum AnymotionCrossFadeSameStateStrategyType {
        Cut, // 从头播放
        NothingTodo, // 不理会
        FastToStart, // 先快速过渡到头, 再从头播放
        FadeToStartHalf, // 将过渡时长分成两半, 向头过渡(不一定过渡到头)一半, 从半中间继续播放
        
        Default = NothingTodo,
    }

    public static class AnymotionCrossFadeSameStateStrategyTypeExtention {

        public static bool IsRestartSameState(this AnymotionCrossFadeSameStateStrategyType type) {
            return type != AnymotionCrossFadeSameStateStrategyType.NothingTodo;
        }
    }

}