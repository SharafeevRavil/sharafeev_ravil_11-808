/*
 * Решается задача: 
 * Cond2 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
 * Автор решения: Шарафеев Равиль, 11-808
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3 {
	class Program {
		static void Main(string[] args) {
			int[] bar = new int[3];
			string[] input = Console.ReadLine().Split();
			for (int i = 0; i < 3; i++) {
				bar[i] = int.Parse(input[i]);
			}
			Array.Sort(bar);

			int[] hole = new int[2];
			input = Console.ReadLine().Split();
			for (int i = 0; i < 2; i++) {
				hole[i] = int.Parse(input[i]);
			}
			Array.Sort(hole);

			Console.WriteLine($"Брус {(bar[1] <= hole[1] && bar[0] <= hole[0] ? "" : "не ")}пролезет");
			Console.ReadKey();
		}
	}
}
