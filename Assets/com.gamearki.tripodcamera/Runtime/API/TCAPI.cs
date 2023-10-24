using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.API {

    public class TCAPI {

        TCStateEffectAPI stateEffectAPI;
        public ITCStateEffectAPI StateEffectAPI => StateEffectAPI;

        TCFollowAPI followAPI;
        public ITCFollowAPI FollowAPI => followAPI;

        TCLookAtAPI lookAtAPI;
        public ITCLookAtAPI LookAtAPI => lookAtAPI;

        TCLowLevelAPI lowLevelAPI;
        public ITCLowLevelAPI LowLevelAPI => lowLevelAPI;

        TCStrategyAPI strategyAPI;
        public ITCStrategyAPI StrategyAPI => strategyAPI;

        TCCameraAPI cameraAPI;
        public ITCCameraAPI CameraAPI => cameraAPI;

        TCDirectorAPI directorAPI;
        public ITCDirectorAPI DirectorAPI => directorAPI;

        TCMathAPI mathAPI;
        public ITCMathAPI MathAPI => mathAPI;

        public TCAPI() {
            this.stateEffectAPI = new TCStateEffectAPI();
            this.followAPI = new TCFollowAPI();
            this.lookAtAPI = new TCLookAtAPI();
            this.lowLevelAPI = new TCLowLevelAPI();
            this.strategyAPI = new TCStrategyAPI();
            this.cameraAPI = new TCCameraAPI();
            this.directorAPI = new TCDirectorAPI();
            this.mathAPI = new TCMathAPI();
        }

        public void Inject(TCRootDomain rootDomain) {
            this.stateEffectAPI.Inject(rootDomain.StateEffectDomain);
            this.followAPI.Inject(rootDomain.FollowDomain);
            this.lookAtAPI.Inject(rootDomain.LookAtDomain);
            this.lowLevelAPI.Inject(rootDomain.LowLevelDomain);
            this.strategyAPI.Inject(rootDomain.StrategyDomain);
            this.cameraAPI.Inject(rootDomain.CameraDomain);
            this.directorAPI.Inject(rootDomain.DirectorDomain);
            this.mathAPI.Inject();
        }

    }

}