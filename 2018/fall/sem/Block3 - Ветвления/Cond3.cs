/*
 * Решается задача: 
 * Cond3 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
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
			int first = int.Parse(Console.ReadLine());
			int second = first % 1000;
			first /= 1000;

			if(GetSum(second) - GetSum(first) == 1) {
				if (second % 10 == 0) {
					Console.WriteLine("No");
				}
				else {
					Console.WriteLine("Yes");
				}
			}
			else {
				if (second % 10 == 9) {
					Console.WriteLine("No");
				}
				else {
					Console.WriteLine("Yes");
				}
			}

			Console.WriteLine( ((GetSum(second) - GetSum(first) == 1) ? (second % 10 != 0) : (second % 10 !=9)) ? "Yes" : "No");

			Console.ReadKey();
		}

		static int GetSum(int a) {
			int sum = 0;
			while (a > 0) {
				sum += a % 10;
				a /= 10;
			}
			return sum;
		}
	}
}
