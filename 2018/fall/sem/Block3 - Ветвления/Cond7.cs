/*
 * Решается задача: 
 * Cond7 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
 * Автор решения: Шарафеев Равиль, 11-808
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3 {
	class Program {
		const double Eps = double.Epsilon;

		static void Main(string[] args) {
			string[] input = Console.ReadLine().Split();
			double x = double.Parse(input[0]);
			double y = double.Parse(input[1]);
			double n = double.Parse(input[2]);

			if (y >= x) {
				Console.WriteLine(0);
				return;
			}

			Console.WriteLine(Math.Ceiling(n * ((x + 0.05 - Eps) - (y + 0.05 - Eps)) / (y + 0.05 - Eps - 1)));
		}
	}
}
