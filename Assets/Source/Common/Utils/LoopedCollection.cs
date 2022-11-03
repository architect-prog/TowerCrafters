using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Source.Common.Utils
{
    public class LoopedCollection<T> : IList<T>
    {
        private readonly List<T> collection;

        public bool IsReadOnly => false;
        public int Count => collection.Count;
        public T this[int index]
        {
            get => collection[index % collection.Count];
            set => collection[index] = value;
        }

        public LoopedCollection()
        {
            collection = Enumerable.Empty<T>().ToList();
        }

        public LoopedCollection(IEnumerable<T> collection)
        {
            var initialCollection = collection ?? Enumerable.Empty<T>();
            this.collection = initialCollection.ToList();
        }

        public void Add(T item)
        {
            collection.Add(item);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(T item)
        {
            var result = collection.Contains(item);
            return result;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var result = collection.Remove(item);
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var result = collection.GetEnumerator();
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var result = collection.GetEnumerator();
            return result;
        }

        public int IndexOf(T item)
        {
            var result = collection.IndexOf(item);
            return result;
        }

        public void Insert(int index, T item)
        {
            collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }
    }
}