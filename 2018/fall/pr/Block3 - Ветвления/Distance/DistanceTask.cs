/*
 * Решается задача: 
 * https://ulearn.me/Course/BasicProgramming/Praktika_Rasstoyanie_do_otrezka__da80ee9b-a2f1-4648-87c2-1a8170561bf0
 * Автор решения: Шарафеев Равиль, 11-808
 */

using System;

namespace DistanceTask {
	public static class DistanceTask {
		const double Eps = 10e-5;
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y) {
			Vector2D first = new Vector2D(ax, ay);
			Vector2D second = new Vector2D(bx, by);
			Vector2D point = new Vector2D(x, y);
			Vector2D projection = Geometry.GetProjection(first, second, point);
			//Если проекция на прямую AB лежит на отрезке AB, то искомое растояние - расстояние от данной точки до проекции,
			//иначе искомое расстояние - минимальное расстояние между точкой и одного из конца отрезков
			if (Math.Abs((projection - first).Length + (projection - second).Length - (first - second).Length) <= Eps) {
				return (point - projection).Length;
			}
			else {
				return Math.Min((point - first).Length, (point - second).Length);
			}
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

		//Перегрузки необходимых операторов
		public static Vector2D operator +(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		}
		public static Vector2D operator -(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		}
		public static Vector2D operator *(Vector2D v1, int k) {
			return new Vector2D(v1.X * k, v1.Y * k);
		}
		public static Vector2D operator *(int k, Vector2D v1) {
			return new Vector2D(v1.X * k, v1.Y * k);
		}
		public static Vector2D operator *(Vector2D v1, double k) {
			return new Vector2D(v1.X * k, v1.Y * k);
		}
		public static Vector2D operator *(double k, Vector2D v1) {
			return new Vector2D(v1.X * k, v1.Y * k);
		}
		//Нормализация вектора
		public Vector2D Normalize() {
			double norm = Math.Sqrt(X * X + Y * Y);
			X /= norm;
			Y /= norm;
			return this;
		}
	}

	public static class Geometry {
		public static Vector2D GetProjection(Vector2D a, Vector2D b, Vector2D x) {
			//Получение проекции через скалярное произведение
			Vector2D v = b - a;
			v.Normalize();
			double f = ScalVecMultiply(v, x - a);
			return a + v * f;
		}

		//Скалярное произведение
		public static double ScalVecMultiply(Vector2D v1, Vector2D v2) {
			return v1.X * v2.X + v1.Y * v2.Y;
		}
	}
}