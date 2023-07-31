using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorEntity {

        TCDirectorFSMComponent fsmComponent;
        public TCDirectorFSMComponent FSMComponent => fsmComponent;

        public TCDirectorEntity() {
            fsmComponent = new TCDirectorFSMComponent();
        }
    }

}