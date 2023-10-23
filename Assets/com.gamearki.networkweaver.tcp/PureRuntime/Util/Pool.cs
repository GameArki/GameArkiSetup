using System;
using System.Collections.Generic;

namespace GameArki.Network {

    public class Pool<T> {

        Stack<T> objs;
        Func<T> generator;

        public Pool() {
            objs = new Stack<T>(20);
        }

        public Pool(Func<T> func) : this() {
            generator = func;
        }

        public void SetGenerator(Func<T> func) {
            generator = func;
        }

        public T Take() {
            return objs.Count == 0 ? generator() : objs.Pop();
        }

        public void Return(T obj) {
            objs.Push(obj);
        }

        public void Clear() {
            objs.Clear();
        }
    }
}
