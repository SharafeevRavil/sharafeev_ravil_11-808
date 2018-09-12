using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr7 {
	class Program {
		static void Main(string[] args) {
			int k = Int32.Parse(Console.ReadLine());
			int b = Int32.Parse(Console.ReadLine());

			Vector2D paral = new Vector2D(1, k);
			Vector2D perp = paral.Perp();

			Console.WriteLine($"Parallel vector: {paral.X}, {paral.Y}");
			Console.WriteLine($"Perp vector: {perp.X}, {perp.Y}");
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

		public Vector2D Perp() {
			return new Vector2D(-Y, X);
		}
	}
}
