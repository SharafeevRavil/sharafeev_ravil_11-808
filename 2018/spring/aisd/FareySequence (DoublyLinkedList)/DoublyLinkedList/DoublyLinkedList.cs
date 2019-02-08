using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList {
	public class DoublyLinkedList<T> : IEnumerable<T> {
		public DoublyLinkedList() {
			First = null;
			Last = null;
			Count = 0;
		}

		public DoublyLinkedListNode<T> First {
			get;
			private set;
		}

		public DoublyLinkedListNode<T> Last {
			get;
			private set;
		}

		public int Count {
			get;
			private set;
		}

		public void AddAfter(DoublyLinkedListNode<T> node, T value) {
			DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(value, node, node.Next);
			newNode.Previous.Next = newNode;
			newNode.Next.Previous = newNode;
			Count++;
		}

		public void AddAfter(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode) {
			newNode.Next = node.Next;
			newNode.Previous = node;
			newNode.Previous.Next = newNode;
			newNode.Next.Previous = newNode;
			Count++;
		}

		public void AddBefore(DoublyLinkedListNode<T> node, T value) {
			DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(value, node.Previous, node);
			newNode.Previous.Next = newNode;
			newNode.Next.Previous = newNode;
			Count++;
		}

		public void AddBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode) {
			newNode.Next = node;
			newNode.Previous = node.Previous;
			newNode.Previous.Next = newNode;
			newNode.Next.Previous = newNode;
			Count++;
		}

		public void AddFirst(T value) {
			if (Count == 0) {
				First = new DoublyLinkedListNode<T>(value, null, null);
				Last = First;
			}
			else {
				DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(value, null, First);
				First.Next = newNode;
			}
			Count++;
		}

		public void AddFirst(DoublyLinkedListNode<T> newNode) {
			if (Count == 0) {
				First = newNode;
				newNode.Next = null;
				newNode.Previous = null;
				Last = First;
			}
			else {
				newNode.Next = First;
				newNode.Previous = null;
				First.Previous = newNode;
			}
			Count++;
		}

		public void AddLast(T value) {
			if (Count == 0) {
				First = new DoublyLinkedListNode<T>(value, null, null);
				Last = First;
			}
			else {
				DoublyLinkedListNode<T> newNode = new DoublyLinkedListNode<T>(value, Last, null);
				Last.Next = newNode;
			}
			Count++;
		}

		public void AddLast(DoublyLinkedListNode<T> newNode) {
			if (Count == 0) {
				First = newNode;
				newNode.Next = null;
				newNode.Previous = null;
				Last = First;
			}
			else {
				newNode.Previous = Last;
				newNode.Next = null;
				Last.Next = newNode;
			}
			Count++;
		}

		public void RemoveFirst() {
			First.Next.Previous = null;
			First = First.Next;
			Count--;
		}

		public void RemoveLast() {
			Last.Previous.Next = null;
			Last = Last.Previous;
			Count--;
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator() {
			return new ListEnumerator<T>(First);
		}

		public class ListEnumerator<T> : IEnumerator<T> {
			public ListEnumerator(DoublyLinkedListNode<T> node) {
				first = node;
				Reset();
			}

			private DoublyLinkedListNode<T> curNode;
			private DoublyLinkedListNode<T> first;

			public object Current {
				get {
					return ((IEnumerator<T>)this).Current;
				}
			}

			T IEnumerator<T>.Current {
				get {
					if (curNode == null) {
						throw new InvalidOperationException();
					}
					return curNode.Value;
				}
			}

			public bool MoveNext() {
				if (curNode.Next == null) {
					return false;
				}
				else {
					curNode = curNode.Next;
					return true;
				}
			}

			public void Reset() {
				curNode = first;
			}

			public void Dispose() {

			}
		}
	}
}
