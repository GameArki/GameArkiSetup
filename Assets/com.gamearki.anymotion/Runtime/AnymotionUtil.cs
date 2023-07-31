namespace GameArki.Anymotion {

    public static class AnymotionUtil {

        public static void InitializeAndPlayDefault(int layer, AnymotionGo go, AnymotionSO so) {
            // Step 1: 初始化
            go.Ctor();

            // Step 2: 添加 State
            var all = so.transitions;
            for (int i = 0; i < all.Length; i += 1) {
                var trans = all[i];
                go.Add(layer, trans.stateName, trans.clip, trans.fadeInSec);
                if (trans.isDefault) {
                    // Step 3: 设置默认 State
                    go.SetDefaultState(layer, trans.stateName);
                }
            }

            // Setp 4: 结束添加
            go.EndAdd(layer); // 结束添加

            // Step 5: 播放 State
            go.PlayDefault(layer, true); // 播放 State
        }
    }
}