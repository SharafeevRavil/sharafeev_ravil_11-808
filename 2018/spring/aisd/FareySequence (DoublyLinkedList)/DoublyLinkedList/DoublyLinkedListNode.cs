using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList {
	public class DoublyLinkedListNode<T> {
		public DoublyLinkedListNode<T> Next {
			get; set;
		}

		public DoublyLinkedListNode<T> Previous {
			get; set;
		}

		public T Value {
			get; set;
		}

		public DoublyLinkedListNode(T value, DoublyLinkedListNode<T> previous, DoublyLinkedListNode<T> next) {
			Value = value;
			Previous = previous;
			Next = next;
		}
	}
}
