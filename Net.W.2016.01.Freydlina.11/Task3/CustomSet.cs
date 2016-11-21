using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Task2;

namespace Task3
{
    public class CustomSet<T>: IEnumerable<T> where T: class
    {
        public CustomCollection<T> Collection { get; }
        public int Count => Collection.Count;

        public CustomSet(int size = 0)
        {
            Collection = new CustomCollection<T>(size);
        }

        public CustomSet(T[] elements): this(elements.Length+1)
        {
            Collection = new CustomCollection<T>(elements);
        }

        public CustomSet(CustomSet<T> set)
        {
            Collection = new CustomCollection<T>(set.Collection);
        }

        public void Add(T item)
        {
            bool permit = true;
            for (int i = 0; i <= Collection.Count; i++)
                if (Collection.Elements[i].Equals(item))
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
                if (set1.Collection.Contains(set2.Collection.Elements[i]))
                    result.Add(set2.Collection.Elements[i]);
            return result;
        }

        public static CustomSet<T> Union(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>(set1);
            
            for (int i = 0; i < set2.Count; i++)
                result.Add(set2.Collection.Elements[i]);
            return result;
        }

        public static CustomSet<T> Except(CustomSet<T> set1, CustomSet<T> set2)
        {
            if (set1 == null || set2 == null) throw new ArgumentNullException();
            CustomSet<T> result = new CustomSet<T>(set1);

            for (int i = 0; i < set2.Count; i++)
                result.Remove(set2.Collection.Elements[i]);
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
