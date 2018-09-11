using System;
using static System.Math;

namespace Expr6 {
	class Program {
		static void Main(string[] args) {
			Vector2D v0 = new Vector2D(Console.ReadLine().Split());
			Vector2D v1 = new Vector2D(Console.ReadLine().Split());
			Vector2D v2 = new Vector2D(Console.ReadLine().Split());

			Console.WriteLine( 
				Abs(
					(v2.Y - v1.Y)*v0.X - (v2.X-v1.X)*v0.Y+ v2.X*v1.Y - v2.Y*v1.X
				) / Sqrt((v2.Y - v1.Y) * (v2.Y - v1.Y) + (v2.X - v1.X) * (v2.X - v1.X))
			);
			Console.ReadKey();
		}
	}

	public class Vector2D {
		public float X;
		public float Y;

		public Vector2D(string[] args) {
			X = float.Parse(args[0]);
			Y = float.Parse(args[1]);
		}
	}
}
