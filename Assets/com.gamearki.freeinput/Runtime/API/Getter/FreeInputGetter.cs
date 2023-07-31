using UnityEngine;
using GameArki.FreeInput.Facades;

namespace GameArki.FreeInput.API.Getter {

    public class FreeInputGetter : IFreeInputGetter {

        FreeInputCore core;

        FreeInputFacades facades;

        public FreeInputGetter() { }

        public void Inject(FreeInputFacades facades) {
            this.facades = facades;
        }

        bool IFreeInputGetter.GetDown(ushort bindID) {
            var domain = facades.MainDomain;
            return domain.GetDown(bindID);
        }

        bool IFreeInputGetter.GetPressing(ushort bindID) {
            var domain = facades.MainDomain;
            return domain.GetPressing(bindID);
        }

        bool IFreeInputGetter.GetUp(ushort bindID) {
            var domain = facades.MainDomain;
            return domain.GetUp(bindID);
        }

        KeyCode IFreeInputGetter.GetCurrentDownKeyCode() {
            var domain = facades.MainDomain;
            return domain.GetCurrentDownKeyCode();
        }

        KeyCode IFreeInputGetter.GetCurrentPressingKeyCode() {
            var domain = facades.MainDomain;
            return domain.GetCurrentPressingKeyCode();
        }

        KeyCode IFreeInputGetter.GetCurrentUpKeyCode() {
            var domain = facades.MainDomain;
            return domain.GetCurrentUpKeyCode();
        }

    }

}