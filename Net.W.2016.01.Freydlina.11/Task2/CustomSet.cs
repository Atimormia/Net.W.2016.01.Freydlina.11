using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class CustomSet<T>: IEnumerable<T> where T: class
    {
        public CustomCollection<T> Collection { get; }
        /// <summary>
        /// Items count in set
        /// </summary>
        public int Count => Collection.Count;

        /// <summary>
        /// Really size of set
        /// </summary>
        public int Size => Collection.Size;

        /// <summary>
        /// Creates set with specified size
        /// </summary>
        /// <param name="size"></param>
        public CustomSet(int size = 0)
        {
            Collection = new CustomCollection<T>(size);
        }

        //public CustomSet(T[] elements) : this(elements.Length + 1)
        //{
        //    Collection = new CustomCollection<T>(elements);
        //}

        /// <summary>
        /// Creates set from another set
        /// </summary>
        /// <param name="set"></param>
        public CustomSet(CustomSet<T> set)
        {
            Collection = new CustomCollection<T>(set.Collection);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CustomSet<T>)) return false;
            if (ReferenceEquals(this, obj)) return true;

            CustomSet<T> customSetObj = (CustomSet<T>)obj;
            if (Count != customSetObj.Count) return false;
            return !customSetObj.Where((t, i) => !Collection.Contains(customSetObj.Collection[i])).Any();
            //for (int i = 0; i < customSetObj.Count; i++)
            //    if (!Collection.Contains(customSetObj.Collection[i]))
            //        return false;
            //return true;
        }

        protected bool Equals(CustomSet<T> other) => Equals(Collection, other.Collection);
        
        public override int GetHashCode() => Collection?.GetHashCode() ?? 0;

        /// <summary>
        /// Adds item if it does not contain in set
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            bool permit = true;
            for (int i = 0; i < Collection.Count; i++)
                if (Collection[i].Equals(item))
                    permit = false;
            if (permit)
                Collection.Add(item);
        }

        /// <summary>
        /// Removes specified item from set
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item) => Collection.Remove(item);
        
        /// <summary>
        /// Returns set of items each set contains
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <returns></returns>
        public static CustomSet<T> Intersect(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>();
            for (int i = 0; i < set1.Count; i++)
                if (set1.Collection.Contains(set2.Collection[i]))
                    result.Add(set2.Collection[i]);
            return result;
        }

        /// <summary>
        /// Returns set of all items of two sets
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <returns></returns>
        public static CustomSet<T> Union(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>(set1);
            
            for (int i = 0; i < set2.Count; i++)
                result.Add(set2.Collection[i]);
            return result;
        }

        /// <summary>
        /// Returns set of items of first set without items of second set
        /// </summary>
        /// <param name="set1"></param>
        /// <param name="set2"></param>
        /// <returns></returns>
        public static CustomSet<T> Except(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>(set1);

            for (int i = 0; i < set2.Count; i++)
                result.Remove(set2.Collection[i]);
            return result;
        }

        public IEnumerator<T> GetEnumerator() => Collection;
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Trim() => Collection.Trim();

    }
}
