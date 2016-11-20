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

        public CustomCollection(int size)
        {
            Elements = new T[size];
        }

        public CustomCollection(int size, T[] elements): this(size)
        {
            int minLen = elements.Length < Elements.Length ? elements.Length : Elements.Length;
            for (int i = 0; i < minLen; i++)
                Elements[i] = elements[i];
        }

        public CustomCollection(T[] elements) : this(elements.Length, elements)
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

        public void Resize()
        {
            int newSize = Elements.Length > 0 ? Elements.Length*2 : 2;
            Resize(newSize);
        }

        public void Resize(int newSize)
        {
            Elements = new CustomCollection<T>(newSize, Elements).Elements;
        }

        object IEnumerator.Current => Current;
    }
}
