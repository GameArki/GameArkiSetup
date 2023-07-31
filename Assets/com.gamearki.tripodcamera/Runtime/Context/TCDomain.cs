using GameArki.TripodCamera.Facades;

namespace GameArki.TripodCamera.Domain {

    public class TCDomain {

        TCCameraDomain cameraDomain;
        public TCCameraDomain CameraDomain => cameraDomain;

        TCApplyDomain applyDomain;
        public TCApplyDomain ApplyDomain => applyDomain;

        TCDirectorDomain directorDomain;
        public TCDirectorDomain DirectorDomain => directorDomain;

        public TCDomain() {
            this.cameraDomain = new TCCameraDomain();
            this.applyDomain = new TCApplyDomain();
            this.directorDomain = new TCDirectorDomain();
        }

        public void Inject(TCContext context) {
            cameraDomain.Inject(context);
            applyDomain.Inject(context, cameraDomain, directorDomain);
            directorDomain.Inject(context);
        }

    }

}