using System;
using System.Collections;
using System.Collections.Generic;

namespace Task2
{
    public class CustomQueue<T>: IEnumerable<T>
    {
        private CustomCollection<T> collection;
        private int head;
        private int tail;

        public int Size => collection.Elements.Length;

        public CustomQueue(int startSize = 5)
        {
            collection = new CustomCollection<T>(startSize);
            head = 0;
            tail = 0;
        }

        public CustomQueue(T[] elements): this(elements.Length)
        {
            for (int i = 0; i < elements.Length; i++)
                Push(elements[i]);
        }

        public bool IsEmpty() => head == tail;

        public void Push(T item)
        {
            if (Count() >= Size-1)
                collection.Resize();
            collection.Elements[tail] = item;
            tail = (tail + 1) % Size;
        }

        public T Pop()
        {
            if (IsEmpty())
                throw new NullReferenceException();
            T result = collection.Elements[head];
            head = (head + 1) % Size;
            return result;
        }

        public T Peek()
        {
            return collection.Elements[head];
        }

        public int Count()
        {
            if (head > tail)
                return Size - head + tail;
            return tail - head;
        }

        public void Clear()
        {
            collection = new CustomCollection<T>(Size);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return collection;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
