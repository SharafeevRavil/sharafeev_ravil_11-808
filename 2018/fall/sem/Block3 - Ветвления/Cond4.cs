/*
 * Решается задача: 
 * Cond4 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
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
			int[] points = new int[4];
			string[] input = Console.ReadLine().Split();
			for (int i = 0; i < 2; i++) {
				points[i] = int.Parse(input[i]);
			}
			input = Console.ReadLine().Split();
			for (int i = 0; i < 2; i++) {
				points[i+2] = int.Parse(input[i]);
			}

			int left = Math.Max(points[0], points[2]);
			int right = Math.Min(points[1], points[3]);

			if (right < left) {
				Console.WriteLine("Пересечения нет");
			}
			else {
				Console.WriteLine($"Пересечение находится между точками {left} и {right}");
			}
			Console.ReadKey();
		}
	}
}
