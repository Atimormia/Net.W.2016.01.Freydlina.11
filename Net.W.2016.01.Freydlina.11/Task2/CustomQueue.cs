using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class CustomQueue<T>: IEnumerable<T>
    {
        private CustomCollection<T> collection;
        private int head;
        private int tail;

        /// <summary>
        /// Contains real collection size including non specified elements
        /// </summary>
        public int Size => collection.Elements.Length;

        /// <summary>
        /// Creates empty queue
        /// </summary>
        /// <param name="startSize">Could be start collection size specified. Start size is 5 by default</param>
        public CustomQueue(int startSize = 5)
        {
            collection = new CustomCollection<T>(startSize);
            head = 0;
            tail = 0;
        }

        /// <summary>
        /// Creates queue from specified array
        /// </summary>
        /// <param name="elements"></param>
        public CustomQueue(IEnumerable<T> elements): this(elements.Count())
        {
            foreach (T t in elements)
                Push(t);
        }

        public bool IsEmpty() => head == tail;

        /// <summary>
        /// Adds element at the tail of queue
        /// </summary>
        /// <param name="item">element</param>
        public void Push(T item)
        {
            if (Count() >= Size-1)
                collection.Resize();
            collection.Elements[tail] = item;
            tail = (tail + 1) % Size;
        }

        /// <summary>
        /// Takes off element from head of queue and returns it
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (IsEmpty())
                throw new NullReferenceException();
            T result = collection.Elements[head];
            head = (head + 1) % Size;
            return result;
        }

        /// <summary>
        /// Returns element from head of queue, but doesn't delete it
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return collection.Elements[head];
        }

        /// <summary>
        /// Count of elements from queue head to tail
        /// </summary>
        /// <returns></returns>
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
