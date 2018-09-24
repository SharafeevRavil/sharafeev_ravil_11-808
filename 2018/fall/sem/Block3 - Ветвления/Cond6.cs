/*
 * Решается задача: 
 * Cond6 https://ulearn.me/Course/BasicProgramming/Zadachi_na_seminar_609e4aa9-0d76-4e33-90a8-b3340b266391
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
			Vector2D[] vectors = new Vector2D[3];
			for (int i = 0; i < 3; i++) {
				string input = Console.ReadLine();
				vectors[i] = new Vector2D(double.Parse(input.Split()[0]), double.Parse(input.Split()[1]));
			}
			for (int i = 0; i < 3; i++) {
				double[] len = new double[3] {
					(vectors[(1 + i) % 3] - vectors[i]).Length,
					(vectors[(2 + i) % 3] - vectors[(1 + i) % 3]).Length,
					(vectors[i] - vectors[(2 + i) % 3]).Length
				};
				if (Math.Sqrt(len[0] * len[0] + len[1] * len[1]) - len[2] < Eps && len[0] - len[1] < Eps) {
					Console.WriteLine($"Точки являются вершинами квадрата. Координаты четвертой точки: " +
						$"{(vectors[(2 + i) % 3] + (vectors[0] - vectors[(1 + i) % 3])).ToString()}");
					Console.ReadKey();
					return;
				}
			}
			Console.WriteLine("Точки не являются вершинами квадрата");
			Console.ReadKey();
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

		public override string ToString() {
			return $"X = {X}, Y = {Y}";
		}

		//Перегрузки необходимых операторов
		public static Vector2D operator +(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		}
		public static Vector2D operator -(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		}
	}
}
