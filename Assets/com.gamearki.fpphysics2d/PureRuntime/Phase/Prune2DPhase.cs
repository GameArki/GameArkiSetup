using FixMath.NET;

namespace GameArki.FPPhysics2D.Phases {

    // 粗筛阶段
    // 简单计算绝对不可能碰撞的物体
    internal class Prune2DPhase {

        FPContext2D context;

        internal Prune2DPhase() {}

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick(in FP64 step) {
            ApplyClear();
            ApplyPrune();
        }

        void ApplyClear() {
            var pruneIgnoreContact2DRepo = context.PruneIgnoreContactRepo;
            pruneIgnoreContact2DRepo.Clear();
        }

        void ApplyPrune() {

        }

    }

}