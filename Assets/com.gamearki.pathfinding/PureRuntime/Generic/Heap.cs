using System;
using System.Collections.Generic;

namespace GameArki.PathFinding.Generic {

    public class Heap<T> {

        T[] items;

        int count;
        public int Count => count;

        int capacity;
        public int Capacity => capacity;

        Comparer<T> comparer;

        public Heap(int capacity) : this(Comparer<T>.Default, capacity) {
        }

        public Heap(Comparer<T> comparer, int capacity) {
            this.comparer = comparer;
            this.capacity = capacity;
            this.count = 0;
            this.items = new T[capacity];
        }

        public void Foreach(Action<T> action) {
            for (int i = 0; i < count; i++) {
                action(items[i]);
            }
        }

        public void Foreach_BFS(Action<T> action) {
            if (count == 0) return;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(0);

            while (queue.Count > 0) {
                var index = queue.Dequeue();
                action(items[index]);

                int leftChildIndex = GetLeftChildIndex(index);
                if (leftChildIndex < count) queue.Enqueue(leftChildIndex);

                int rightChildIndex = GetRightChildIndex(index);
                if (rightChildIndex < count) queue.Enqueue(rightChildIndex);
            }
        }

        public void Foreach_DFS(Action<T> action) {
            Foreach_DFS(action, 0);
        }

        void Foreach_DFS(Action<T> action, int index) {
            if (index >= count) {
                return;
            }

            action(items[index]);
            Foreach_DFS(action, GetLeftChildIndex(index));
            Foreach_DFS(action, GetRightChildIndex(index));
        }

        public void Push(T value) {
            if (count == capacity) {
                throw new Exception("Heap is full");
            }

            items[count] = value;
            count++;

            HeapifyUp();
        }

        void HeapifyUp() {
            int index = count - 1;
            var parentIndex = GetParentIndex(index);
            while (index > 0 && NeedSwap(parentIndex, index)) {
                Swap(parentIndex, index);
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }
        }

        public T Pop() {
            if (count == 0) {
                throw new Exception("Heap is empty");
            }

            var min = items[0];
            items[0] = items[count - 1];
            count--;
            HeapifyDown();
            return min;
        }

        void HeapifyDown() {
            int index = 0;
            while (HasLeftChild(index)) {

                int smallerChildIndex = GetComparedChildIndex(index);

                if (!NeedSwap(index, smallerChildIndex)) {
                    break;
                }

                Swap(index, smallerChildIndex);
                index = smallerChildIndex;
            }
        }

        public void Clear() {
            var defaultT = default(T);
            for (int i = 0; i < count; i++) {
                items[i] = defaultT;
            }
            count = 0;
        }

        int GetParentIndex(int index) {
            return (index - 1) / 2;
        }

        void Swap(int index1, int index2) {
            var temp = items[index1];
            items[index1] = items[index2];
            items[index2] = temp;
        }

        int GetLeftChildIndex(int index) {
            return index * 2 + 1;
        }

        int GetRightChildIndex(int index) {
            return index * 2 + 2;
        }

        int GetComparedChildIndex(int index) {
            int leftChildIndex = GetLeftChildIndex(index);
            int rightChildIndex = GetRightChildIndex(index);
            if (NeedSwap(rightChildIndex, leftChildIndex)) return leftChildIndex;
            return rightChildIndex;
        }

        bool HasLeftChild(int index) {
            return GetLeftChildIndex(index) < count;
        }

        bool HasRightChild(int index) {
            return GetRightChildIndex(index) < count;
        }

        bool NeedSwap(int parentIndex, int childIndex) {
            return comparer.Compare(items[parentIndex], items[childIndex]) >= 0;
        }

    }

}