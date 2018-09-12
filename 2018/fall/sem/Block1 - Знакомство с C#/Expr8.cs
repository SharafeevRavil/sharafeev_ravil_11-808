using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr8 {
	class Program {
		static void Main(string[] args) {
			Vector2D v0 = new Vector2D(Console.ReadLine().Split());//A
			Vector2D v1 = new Vector2D(Console.ReadLine().Split());//L1
			Vector2D v2 = new Vector2D(Console.ReadLine().Split());//L2

			Line2D lineL = new Line2D(v1, v2);
			Vector2D ifParallel = lineL.CheckParallel(v0);
			if (ifParallel != null) {
				Console.WriteLine($"X = {ifParallel.X}, Y = {ifParallel.Y}");
				Console.ReadKey();
				return;
			}
			Line2D linePerp = new Line2D(v0, v0 + (v2 - v1).Perp());


			float x = (lineL.B - linePerp.B) / (linePerp.K - lineL.K);
			float y = lineL.K * x + lineL.B;

			Console.WriteLine($"X = {x}, Y = {y}");
			Console.ReadKey();
		}
	}

	public class Vector2D {
		public float X;
		public float Y;

		public Vector2D(float x, float y) {
			X = x;
			Y = y;
		}

		public Vector2D(string[] args) {
			X = float.Parse(args[0]);
			Y = float.Parse(args[1]);
		}

		public Vector2D Perp() {
			return new Vector2D(-Y, X);
		}

		public static Vector2D operator -(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Vector2D operator +(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		}
	}

	public class Line2D {
		public Vector2D V1;
		public Vector2D V2;

		public Line2D(Vector2D v1, Vector2D v2) {
			V1 = v1;
			V2 = v2;
		}

		public float K {
			get {
				return ((V2.Y - V1.Y) / (V2.X - V1.X));
			}
		}

		public float B {
			get {
				return V1.Y - V1.X * K;
			}
		}

		public Vector2D CheckParallel(Vector2D vector) {
			if (V1.X == V2.X) {
				return new Vector2D(V1.X, vector.Y);
			}
			if (V1.Y == V2.Y) {
				return new Vector2D(vector.X, V1.Y);
			}
			return null;
		}
	}
}
