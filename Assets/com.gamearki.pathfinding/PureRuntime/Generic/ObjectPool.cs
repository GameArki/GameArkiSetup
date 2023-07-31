using System;
using System.Collections.Generic;

namespace GameArki.PathFinding.Generic {

    public class ObjectPool<T> where T : new() {

        readonly Queue<T> objects;

        public int Count => objects.Count;

        readonly int maxSize;

        public ObjectPool(int maxSize) {
            objects = new Queue<T>(maxSize);
            this.maxSize = maxSize;
        }

        public bool TryDequeue(out T obj) {
            return objects.TryDequeue(out obj);
        }

        public void Enqueue(T obj) {
            if (objects.Count >= maxSize) {
                throw new Exception($"ObjPool is full! maxSize:{maxSize}");
            }
            objects.Enqueue(obj);
        }

    }

}
