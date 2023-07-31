using UnityEngine;
using GameArki.FreeInput.Facades;

namespace GameArki.FreeInput.API.Setter {

    public class FreeInputSetter : IFreeInputSetter {

        FreeInputFacades facades;

        public void Inject(FreeInputFacades facades) {
            this.facades = facades;
        }

        void IFreeInputSetter.Bind(ushort bindID, KeyCode keyCode) {
            var domain = facades.MainDomain;
            domain.Bind(bindID, keyCode);
        }

        void IFreeInputSetter.Unbind(ushort bindID, KeyCode keyCode) {
            var domain = facades.MainDomain;
            domain.Unbind(bindID, keyCode);
        }

        void IFreeInputSetter.UnbindAll() {
            var domain = facades.MainDomain;
            domain.UnbindAll();
        }

        void IFreeInputSetter.Rebind(ushort bindID, KeyCode oldKeyCode, KeyCode newKeyCode) {
            var domain = facades.MainDomain;
            domain.RebindKeyCode(bindID, oldKeyCode, newKeyCode);
        }

    }

}