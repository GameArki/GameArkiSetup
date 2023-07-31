using System;
using System.Collections.Generic;
using UnityEngine;
using GameArki.FreeInput.Facades;

namespace GameArki.FreeInput.Domain {

    public class MainDomain {

        FreeInputFacades facades;

        public MainDomain() { }

        public void Inject(FreeInputFacades facades) {
            this.facades = facades;
        }

        public void Bind(ushort bindID, KeyCode keyCode) {
            var dic = facades.bindCodeDic;
            if (!dic.TryGetValue(bindID, out var list)) {
                list = new List<KeyCode>();
                dic.Add(bindID, list);
            }
            list.Add(keyCode);
        }

        public void RebindKeyCode(ushort bindID, KeyCode oldKeyCode, KeyCode newKeyCode) {
            var dic = facades.bindCodeDic;
            if (!dic.TryGetValue(bindID, out var list)) {
                return;
            }

            list.Remove(oldKeyCode);
            list.Add(newKeyCode);
        }

        public void Unbind(ushort bindID, KeyCode keyCode) {
            var dic = facades.bindCodeDic;
            if (!dic.TryGetValue(bindID, out var list)) {
                return;
            }
            list.Remove(keyCode);
        }

        public void UnbindAll() {
            facades.bindCodeDic.Clear();
        }

        public bool GetDown(ushort bindID) {
            var dic = facades.bindCodeDic;
            if (dic.TryGetValue(bindID, out var list)) {
                for (int i = 0; i < list.Count; i++) {
                    if (Input.GetKeyDown(list[i])) {
                        return true;
                    }
                }

            }

            return false;
        }

        public bool GetPressing(ushort bindID) {
            var dic = facades.bindCodeDic;
            if (dic.TryGetValue(bindID, out var list)) {
                for (int i = 0; i < list.Count; i++) {
                    if (Input.GetKey(list[i])) {
                        return true;
                    }
                }

            }

            return false;
        }

        public bool GetUp(ushort bindID) {
            var dic = facades.bindCodeDic;
            if (dic.TryGetValue(bindID, out var list)) {
                for (int i = 0; i < list.Count; i++) {
                    if (Input.GetKeyUp(list[i])) {
                        return true;
                    }
                }

            }

            return false;
        }

        public KeyCode GetCurrentDownKeyCode() {
            var e = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
            while (e.MoveNext()) {
                KeyCode keyCode = (KeyCode)e.Current;
                if (Input.GetKeyDown(keyCode)) {
                    return keyCode;
                }
            }

            return KeyCode.None;
        }

        public KeyCode GetCurrentPressingKeyCode() {
            var e = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
            while (e.MoveNext()) {
                KeyCode keyCode = (KeyCode)e.Current;
                if (Input.GetKey(keyCode)) {
                    return keyCode;
                }
            }

            return KeyCode.None;
        }

        public KeyCode GetCurrentUpKeyCode() {
            var e = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
            while (e.MoveNext()) {
                KeyCode keyCode = (KeyCode)e.Current;
                if (Input.GetKeyUp(keyCode)) {
                    return keyCode;
                }
            }

            return KeyCode.None;
        }

    }

}