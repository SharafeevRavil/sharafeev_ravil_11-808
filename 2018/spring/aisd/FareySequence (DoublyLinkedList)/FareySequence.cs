using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DoublyLinkedList;

namespace ConsoleApp11 {
	public static class FareySequence {
		public static IEnumerable<Tuple<int, int>> Farey(int n) {
			DoublyLinkedList<Tuple<int, int>> list = new DoublyLinkedList<Tuple<int, int>>();
			//LinkedList<Tuple<int, int>> list = new LinkedList<Tuple<int, int>>();

			list.AddFirst(Tuple.Create(0, 1));
			list.AddLast(Tuple.Create(1, 1));
			ModifyList(n, list, list.First);
			return list;
		}
		//рекурсивный метод, который добавляет новый элемент после prevNode, если добавление этого элемента законно, далее вызывает себя 
		//для prevNode и нового узла. выход из рекурсии, когда добавление элемента невозможно, т.е. при знаменателе большем n
		static void ModifyList(int n, DoublyLinkedList<Tuple<int, int>> list, DoublyLinkedListNode<Tuple<int, int>> prevNode) {
			if ((prevNode.Value.Item2 + prevNode.Next.Value.Item2) <= n) {
				list.AddAfter(prevNode, Tuple.Create(
					prevNode.Value.Item1 + prevNode.Next.Value.Item1,
					prevNode.Value.Item2 + prevNode.Next.Value.Item2
				));
				var next = prevNode.Next;
				ModifyList(n, list, prevNode);
				ModifyList(n, list, next);
			}
		}
	}
}
