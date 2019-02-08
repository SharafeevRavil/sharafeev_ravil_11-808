using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DoublyLinkedList;

namespace ConsoleApp11 {
	class Program {
		static void Main(string[] args) {
			while (true) {
				Console.WriteLine("Введите n");
				int n = int.Parse(Console.ReadLine());
				Console.WriteLine();
				foreach (var elem in FareySequence.Farey(n)) {
					Console.Write($"{elem.Item1}/{elem.Item2} ");
				}
			}
		}
	}
}