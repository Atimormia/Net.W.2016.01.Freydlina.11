using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Task2;

namespace Task3
{
    public class CustomSet<T>: IEnumerable<T> where T: class
    {
        public CustomCollection<T> Collection { get; }
        public int Count => Collection.Count;
        public int Size => Collection.Size;

        public CustomSet(int size = 0)
        {
            Collection = new CustomCollection<T>(size);
        }

        //public CustomSet(T[] elements) : this(elements.Length + 1)
        //{
        //    Collection = new CustomCollection<T>(elements);
        //}

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
        
        public void Add(T item)
        {
            bool permit = true;
            for (int i = 0; i < Collection.Count; i++)
                if (Collection[i].Equals(item))
                    permit = false;
            if (permit)
                Collection.Add(item);
        }

        public void Remove(T item) => Collection.Remove(item);
        
        public static CustomSet<T> Intersect(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>();
            for (int i = 0; i < set1.Count; i++)
                if (set1.Collection.Contains(set2.Collection[i]))
                    result.Add(set2.Collection[i]);
            return result;
        }

        public static CustomSet<T> Union(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>(set1);
            
            for (int i = 0; i < set2.Count; i++)
                result.Add(set2.Collection[i]);
            return result;
        }

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
