using System;
using System.Collections.Generic;

namespace GameArki.Network {

    public class Pool<T> {

        Stack<T> objs;
        Func<T> generator;

        public Pool(Func<T> func = null, int capacity = 20) {
            generator = func;
            objs = new Stack<T>(capacity);
        }

        public void SetGenerator(Func<T> func) {
            generator = func;
        }

        public T Take() {
            return objs.Count == 0 ? generator.Invoke() : objs.Pop();
        }

        public void Return(T obj) {
            objs.Push(obj);
        }

        public void Clear() {
            objs.Clear();
        }
    }
}
