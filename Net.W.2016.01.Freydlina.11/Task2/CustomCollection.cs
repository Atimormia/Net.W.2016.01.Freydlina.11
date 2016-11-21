using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class CustomCollection<T> : ICollection<T>, IEnumerator<T>
    {
        public T[] Elements { get; private set; }
        private int end = 0;

        private int pointer = -1;
        public T Current
        {
            get
            {
                if (pointer == -1)
                    throw new InvalidOperationException();
                return Elements[pointer];
            }
        }
        
        /// <summary>
        /// Creates collection with specified size
        /// </summary>
        /// <param name="size"></param>
        public CustomCollection(int size = 0)
        {
            if (size < 0) throw new ArgumentException("Size must be positive");
            Elements = new T[size];
        }

        /// <summary>
        /// Creates collection from specified array
        /// </summary>
        /// <param name="elements">source</param>
        /// <param name="count">count of elements that should be get</param>
        /// <param name="startIndex">element which to start coping</param>
        public CustomCollection(T[] elements, int count, int startIndex = 0): this(count)
        {
            int minLen = elements.Length - startIndex < this.Elements.Length ? elements.Length : this.Elements.Length;
            for (int i = startIndex; i < minLen; i++)
                this.Elements[i] = elements[i];
        }

        public CustomCollection(CustomCollection<T> collection) : this(collection.Elements)
        {}

        public CustomCollection(T[] elements) : this(elements, elements.Length)
        {}

        public void Dispose() {}

        public bool MoveNext()
        {
            if (pointer < Elements.Length - 1)
            {
                pointer++;
                return true;
            }
            return false;
        }

        public void Reset() => pointer = -1;

        /// <summary>
        /// Increases in half size of collection
        /// </summary>
        public void Resize()
        {
            int newSize = Elements.Length > 0 ? Elements.Length*2 : 2;
            Resize(newSize);
        }

        /// <summary>
        /// Increases collection size to specified size
        /// </summary>
        /// <param name="newSize"></param>
        public void Resize(int newSize)
        {
            Elements = new CustomCollection<T>(Elements, newSize).Elements;
        }

        object IEnumerator.Current => Current;
        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (end == Elements.Length - 1) Resize();
            Elements[end] = item;
            end++;
        }

        public void Clear()
        {
            Elements = new T[Elements.Length];
            end = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < end; i++)
                if (Elements[i].Equals(item))
                    return true;
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < end; i++)
                if (Elements[i].Equals(item))
                    return i;
            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Elements.Length; i++)
            {
                Elements[i] = array[i - arrayIndex];
            }
        }

        private void Move(int index)
        {
            if (index < 0) throw new ArgumentException();
            for (int i = index; i <= end; i++)
                Elements[i] = i != Elements.Length - 1 ? Elements[i + 1] : default(T);
            end--;
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;
            Move(IndexOf(item));
            return true;
        }

        public int Count => end + 1;
        public bool IsReadOnly { get; }
    }
}
