using GameArki.TripodCamera.Facades;

namespace GameArki.TripodCamera.Domain {

    public class TCRootDomain {

        TCApplyDomain applyDomain;
        public TCApplyDomain ApplyDomain => applyDomain;

        TCCameraDomain cameraDomain;
        public TCCameraDomain CameraDomain => cameraDomain;

        TCDirectorDomain directorDomain;
        public TCDirectorDomain DirectorDomain => directorDomain;

        TCFollowDomain followDomain;
        public TCFollowDomain FollowDomain => followDomain;

        TCLookAtDomain lookAtDomain;
        public TCLookAtDomain LookAtDomain => lookAtDomain;

        TCLowLevelDomain lowLevelDomain;
        public TCLowLevelDomain LowLevelDomain => lowLevelDomain;

        TCStateEffectDomain stateEffectDomain;
        public TCStateEffectDomain StateEffectDomain => stateEffectDomain;

        TCStrategyDomain strategyDomain;
        public TCStrategyDomain StrategyDomain => strategyDomain;

        public TCRootDomain() {
            this.applyDomain = new TCApplyDomain();
            this.cameraDomain = new TCCameraDomain();
            this.directorDomain = new TCDirectorDomain();
            this.followDomain = new TCFollowDomain();
            this.lookAtDomain = new TCLookAtDomain();
            this.lowLevelDomain = new TCLowLevelDomain();
            this.stateEffectDomain = new TCStateEffectDomain();
            this.strategyDomain = new TCStrategyDomain();
        }

        public void Inject(TCContext context) {
            applyDomain.Inject(context, cameraDomain, directorDomain);
            cameraDomain.Inject(context);
            directorDomain.Inject(context);
            followDomain.Inject(context);
            lookAtDomain.Inject(context);
            lowLevelDomain.Inject(context);
            stateEffectDomain.Inject(context);
            strategyDomain.Inject(context);
        }

    }

}