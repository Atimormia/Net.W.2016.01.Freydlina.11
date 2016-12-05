using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class CustomCollection<T> : ICollection<T>, IEnumerator<T>
    {
        private T[] elements;
        private int end = 0;
        private int pointer = -1;

        public int Size => elements.Length;
        public T this[int i]
        {
            get { return elements[i]; }
            set { elements[i] = value; }
        }
        public int Count => end;
        public bool IsReadOnly { get; }
        public T Current
        {
            get
            {
                if (pointer == -1)
                    throw new InvalidOperationException();
                return elements[pointer];
            }
        }
        
        /// <summary>
        /// Creates collection with specified size
        /// </summary>
        /// <param name="size"></param>
        public CustomCollection(int size = 0)
        {
            if (size < 0) throw new ArgumentException("Size must be positive");
            elements = new T[size];
        }

        /// <summary>
        /// Creates collection from specified array
        /// </summary>
        /// <param name="elements">source</param>
        /// <param name="count">count of elements that should be get</param>
        /// <param name="startIndex">element which to start coping</param>
        public CustomCollection(T[] elements, int count, int startIndex = 0): this(count)
        {
            int minLen = elements.Length - startIndex < this.Size ? elements.Length : this.Size;
            for (int i = startIndex; i < minLen; i++)
                Add(elements[i]);
        }

        public CustomCollection(CustomCollection<T> collection) : this(collection.elements)
        {
            end = collection.Count;
        }

        public CustomCollection(T[] elements) : this(elements, count:elements.Length+1)
        {}

        /// <summary>
        /// Increases in half size of collection
        /// </summary>
        public void Resize()
        {
            int newSize = Size > 0 ? Size*2 : 2;
            Resize(newSize);
        }

        /// <summary>
        /// Increases collection size to specified size
        /// </summary>
        /// <param name="newSize"></param>
        public void Resize(int newSize)
        {
            elements = new CustomCollection<T>(elements, newSize).elements;
        }

        public void Dispose()
        {
            pointer = 0;
        }

        public bool MoveNext()
        {
            if (pointer < Count - 1)
            {
                pointer++;
                return true;
            }
            return false;
        }

        public void Reset() => pointer = -1;

        object IEnumerator.Current => Current;
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < end; i++)
            {
                yield return elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (end == Size - 1 || Size == 0) Resize();
            elements[end] = item;
            end++;
        }

        public void Clear()
        {
            elements = new T[Size];
            end = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < end; i++)
                if (elements[i].Equals(item))
                    return true;
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < end; i++)
                if (elements[i].Equals(item))
                    return i;
            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Size; i++)
            {
                elements[i] = array[i - arrayIndex];
            }
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;
            Move(IndexOf(item));
            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CustomCollection<T>)) return false;
            if (ReferenceEquals(this, obj)) return true;

            CustomCollection<T> customCollectionObj = (CustomCollection<T>) obj;
            if (Count != customCollectionObj.Count) return false;
            for (int i = 0; i < end;i++)
                if (!elements[i].Equals(customCollectionObj[i]))
                    return false;
            return true;
        }

        protected bool Equals(CustomCollection<T> other)
        {
            return Equals(elements, other.elements) && end == other.end && pointer == other.pointer && IsReadOnly == other.IsReadOnly;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = elements?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ IsReadOnly.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Deletes empty items in collection. Saves memory
        /// </summary>
        public void Trim()
        {
            T[] newCollection = new T[Count+1];
            for (int i = 0; i < Count+1; i++)
                newCollection[i] = elements[i];
            elements = newCollection;
        }

        public void SortBy<TKey>(Func<T, TKey> sorter)
        {
            Trim();
            elements = elements.OrderBy(sorter).ToArray();
        }

        private void Move(int index)
        {
            if (index < 0) throw new ArgumentException();
            for (int i = index; i <= end; i++)
                elements[i] = i != Size - 1 ? elements[i + 1] : default(T);
            end--;
        }

    }
}
