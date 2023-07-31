using UnityEngine;

namespace GameArki.FreeInput.API.Setter {

    public interface IFreeInputSetter {

        void Bind(ushort bindID, KeyCode keyCode);

        void Unbind(ushort bindID, KeyCode keyCode);
        void UnbindAll();

        void Rebind(ushort bindID, KeyCode oldKeyCode, KeyCode newKeyCode);

    }

}