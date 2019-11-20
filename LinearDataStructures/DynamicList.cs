using System;
using System.Collections.Generic;
using System.Text;

namespace LinearDataStructures
{
    /// <summary>Dynamic (linked) list class definition</summary>
    public class DynamicList<T>
    {
        private class ListNode
        {
            public T Element { get; set; }
            public ListNode NextNode { get; set; }
            public ListNode(T element)
            {
                Element = element;
                NextNode = null;
            }
            public ListNode(T element, ListNode prevNode)
            {
                Element = element;
                prevNode.NextNode = this;
            }
        }

        private ListNode head;
        private ListNode tail;
        private int count;

        /// <summary>
        /// Gets or sets the element at the specified position
        /// </summary>
        /// <param name="index">
        /// The position of the element [0 … count-1]
        /// </param>
        /// <returns>The item at the specified index</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When an invalid index is specified
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                    "Invalid index: " + index);
                }
                ListNode currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                return currentNode.Element;
            }
            set
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(
                    "Invalid index: " + index);
                }
                ListNode currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                currentNode.Element = value;
            }
        }

        /// <summary>
        /// Gets the count of elements in the list
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
        }

        public DynamicList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        /// <summary>Add element at the end of the list</summary>
        /// <param name="item">The element to be added</param>
        public void Add(T item)
        {
            if (head == null)
            {
                // We have an empty list -> create a new head and tail
                head = new ListNode(item);
                tail = head;
            }
            else
            {
                // We have a non-empty list -> append the item after tail
                ListNode newNode = new ListNode(item, tail);
                tail = newNode;
            }
            count++;
        }

        /// <summary>Removes and returns element on the specified index
        /// </summary>
        /// <param name="index">The index of the element to be removed
        /// </param>
        /// <returns>The removed element</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// if the index is invalid</exception>
        public T RemoveAt(int index)
        {
            if (index >= count || index < 0)
            {
                throw new ArgumentOutOfRangeException(
                "Invalid index: " + index);
            }
            // Find the element at the specified index
            int currentIndex = 0;
            ListNode currentNode = head;
            ListNode prevNode = null;
            while (currentIndex < index)
            {
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }
            // Remove the found element from the list of nodes
            RemoveListNode(currentNode, prevNode);
            // Return the removed element
            return currentNode.Element;
        }

        /// <summary>
        /// Remove the specified node from the list of nodes
        /// </summary>
        /// <param name="node">the node for removal</param>
        /// <param name="prevNode">the predecessor of node</param>
        private void RemoveListNode(ListNode node, ListNode prevNode)
        {
            count--;
            if (count == 0)
            {
                // The list becomes empty -> remove head and tail
                head = null;
                tail = null;
            }
            else if (prevNode == null)
            {
                // The head node was removed --> update the head
                head = node.NextNode;
            }
            else
            {
                // Redirect the pointers to skip the removed node
                prevNode.NextNode = node.NextNode;
            }
            // Fix the tail in case it was removed
            if (ReferenceEquals(tail, node))
            {
                tail = prevNode;
            }
        }

        /// <summary>
        /// Removes the specified item and return its index.
        /// </summary>
        /// <param name="item">The item for removal</param>
        /// <returns>The index of the element or -1 if it does not exist</returns>
        public int Remove(T item)
        {
            // Find the element containing the searched item
            int currentIndex = 0;
            ListNode currentNode = head;
            ListNode prevNode = null;
            while (currentNode != null)
            {
                if (Equals(currentNode.Element, item))
                {
                    break;
                }
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }
            if (currentNode != null)
            {
                // The element is found in the list -> remove it
                RemoveListNode(currentNode, prevNode);
                return currentIndex;
            }
            else
            {
                // The element is not found in the list -> return -1
                return -1;
            }
        }

        /// <summary>Searches for given element in the list</summary>
        /// <param name="item">The item to be searched</param>
        /// <returns>
        /// The index of the first occurrence of the element
        /// in the list or -1 when it is not found
        /// </returns>
        public int IndexOf(T item)
        {
            int index = 0;
            ListNode currentNode = head;
            while (currentNode != null)
            {
                if (Equals(currentNode.Element, item))
                {
                    return index;
                }
                currentNode = currentNode.NextNode;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Checks if the specified element exists in the list
        /// </summary>
        /// <param name="item">The item to be checked</param>
        /// <returns>
        /// True if the element exists or false otherwise
        /// </returns>
        public bool Contains(T item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }
    }
}
