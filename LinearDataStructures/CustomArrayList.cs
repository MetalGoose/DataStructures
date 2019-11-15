using System;
using System.Collections.Generic;
using System.Text;

namespace LinearDataStructures
{
    class CustomArrayList<T>
    {
        private T[] arr;
        private const int INITIAL_CAPACITY = 4;

        /// <summary>Returns the actual list length</summary>
        public int Count { get; private set; }

        /// <summary>
        /// Initializes the array-based list – allocate memory
        /// </summary>
        public CustomArrayList(int capacity = INITIAL_CAPACITY)
        {
            arr = new T[capacity];
            Count = 0;
        }

        /// <summary>
        /// Doubles the size of this.arr (grow) if it is full
        /// </summary>
        private void GrowIfArrIsFull()
        {
            if (Count + 1 > arr.Length)
            {
                T[] extendedArr = new T[arr.Length * 2];
                Array.Copy(arr, extendedArr, Count);
                arr = extendedArr;
            }
        }

        /// <summary>Adds element to the list</summary>
        /// <param name="item">The element you want to add</param>
        public void Add(T item)
        {
            GrowIfArrIsFull();
            arr[Count] = item;
            Count++;
        }

        /// <summary>
        /// Inserts the specified element at given position in this list
        /// </summary>
        /// <param name="index">
        /// Index, at which the specified element is to be inserted
        /// </param>
        /// <param name="item">Element to be inserted</param>
        /// <exception cref="System.IndexOutOfRangeException">Index is invalid</exception>
        public void Insert(int index, T item)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException(
                "Invalid index: " + index);
            }
            GrowIfArrIsFull();
            Array.Copy(arr, index,
            arr, index + 1, Count - index);
            arr[index] = item;
            Count++;
        }

        /// <summary>Clears the list (remove everything)</summary>
        public void Clear()
        {
            arr = new T[INITIAL_CAPACITY];
            Count = 0;
        }

        /// <summary>
        /// Returns the index of the first occurrence of the specified
        /// element in this list (or -1 if it does not exist).
        /// </summary>
        /// <param name="item">The element you are searching</param>
        /// <returns>
        /// The index of a given element or -1 if it is not found
        /// </returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (Equals(item, arr[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>Checks if an element exists</summary>
        /// <param name="item">The item to be checked</param>
        /// <returns>If the item exists</returns>
        public bool Contains(T item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }

        /// <summary>Indexer: access to element at given index</summary>
        /// <param name="index">Index of the element</param>
        /// <returns>The element at the specified position</returns>
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                    "Invalid index: " + index);
                }
                return arr[index];
            }
            set
            {
                if (index >= Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                    "Invalid index: " + index);
                }
                arr[index] = value;
            }
        }

        /// <summary>Removes the element at the specified index
        /// </summary>
        /// <param name="index">The index of the element to remove
        /// </param>
        /// <returns>The removed element</returns>
        public T RemoveAt(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException(
                "Invalid index: " + index);
            }
            T item = arr[index];
            Array.Copy(arr, index + 1,
            arr, index, Count - index - 1);
            arr[Count - 1] = default(T);
            Count--;
            return item;
        }

        /// <summary>Removes the specified item</summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>The removed item's index or -1 if the item does not exist</returns>
        public int Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
            }
            return index;
        }
    }
}
