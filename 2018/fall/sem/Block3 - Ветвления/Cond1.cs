/*
 * Решается задача: 
 * Cond1 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
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
			Vector2D[] v = new Vector2D[2];
			for (int i = 0; i < 2; i++) {
				string[] input = Console.ReadLine().Split();
				v[i] = new Vector2D(double.Parse(input[0]), double.Parse(input[1]));
			}

			if (!CheckAll(v[0], v[1])) {
				Console.WriteLine("Ход некорректен для всех фигур: начальная и конечная клетки совпадают");
				Console.ReadKey();
				return;
			}

			Console.WriteLine($"Для слона ход {(CheckBishop(v[0], v[1]) ? "корректен" : "некорректен")}");
			Console.WriteLine($"Для коня ход {(CheckKnight(v[0], v[1]) ? "корректен" : "некорректен")}");
			Console.WriteLine($"Для ладьи ход {(CheckCastle(v[0], v[1]) ? "корректен" : "некорректен")}");
			Console.WriteLine($"Для ферзя ход {(CheckQueen(v[0], v[1]) ? "корректен" : "некорректен")}");
			Console.WriteLine($"Для короля ход {(CheckKing(v[0], v[1]) ? "корректен" : "некорректен")}");
			Console.ReadKey();
		}

		//Проверка того, что ход не был совершен в ту же точку
		static bool CheckAll(Vector2D v1, Vector2D v2) {
			if ((v1 - v2).X == 0 && (v1 - v2).Y == 0) {
				return false;
			}
			return true;
		}

		//Проверка правильности хода слона
		static bool CheckBishop(Vector2D v1, Vector2D v2) {
			return Math.Abs((v1 - v2).X) == Math.Abs((v1 - v2).Y);
		}

		//Проверка правильности хода коня
		static bool CheckKnight(Vector2D v1, Vector2D v2) {
			return (v1 - v2).Length == 3 && !CheckCastle(v1, v2) && !CheckBishop(v1, v2);
		}

		//Проверка правильности хода ладьи
		static bool CheckCastle(Vector2D v1, Vector2D v2) {
			return (v1 - v2).X != 0 && (v1 - v2).Y == 0 || (v1 - v2).X == 0 && (v1 - v2).X != 0;
		}

		//Проверка правильности хода ферзя
		static bool CheckQueen(Vector2D v1, Vector2D v2) {
			return CheckBishop(v1, v2) && CheckCastle(v1, v2);
		}

		//Проверка правильности хода короля
		static bool CheckKing(Vector2D v1, Vector2D v2) {
			return (v1 - v2).Length == 1;
		}
	}

	public class Vector2D {
		public double X;
		public double Y;

		public Vector2D(double x, double y) {
			X = x;
			Y = y;
		}

		public double Length {
			get => Math.Sqrt(X * X + Y * Y);
		}

		public static Vector2D operator +(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		}
		public static Vector2D operator -(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		}
	}
}
