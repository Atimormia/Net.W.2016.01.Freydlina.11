using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class CustomCollection<T> : IEnumerator<T>
    {
        public T[] Elements { get; set; }

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
        public CustomCollection(int size)
        {
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
            int minLen = elements.Length - startIndex < Elements.Length ? elements.Length : Elements.Length;
            for (int i = startIndex; i < minLen; i++)
                Elements[i] = elements[i];
        }
        
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
    }
}
