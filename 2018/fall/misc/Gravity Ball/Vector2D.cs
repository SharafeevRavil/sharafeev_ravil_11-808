using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravityBalls {
	public class Vector2D {
		public enum Direction {
			Vertical, Horizontal
		}

		private double x, y;

		public double X {
			get => x;
			set => x = value;
		}

		public double Y {
			get => y;
			set => y = value;
		}

		public double Length {
			get => Math.Sqrt(X * X + Y * Y);
		}

		public void Scale(double k) {
			x = x * k;
			y = y * k;
		}

		public Vector2D(double x, double y) {
			this.x = x;
			this.y = y;
		}

		public void Bounce(Direction direction, double k) {
			if (direction == Direction.Horizontal) {
				y *= -1;
			}
			else {
				x *= -1;
			}
			Scale((1 - k));
		}

		public Vector2D Normalize() {
			double len = Length;
			x /= len;
			y /= len;
			return this;
		}

		public override string ToString() {
			return $"X = {x}, Y = {y}";
		}

		public static Vector2D operator *(Vector2D vec, double k) {
			return new Vector2D(vec.x * k, vec.y * k);
		}
		public static Vector2D operator /(Vector2D vec, double k) {
			return new Vector2D(vec.x / k, vec.y / k);
		}
		public static Vector2D operator +(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.x + v2.x, v1.y + v2.y);
		}
		public static Vector2D operator -(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.x - v2.x, v1.y - v2.y);
		}

		public static Vector2D Random(Random rand) {
			double x = rand.NextDouble() * (rand.Next() % 2 == 0 ? 1 : -1);
			double y = Math.Sqrt(1 - x * x) * (rand.Next() % 2 == 0 ? 1 : -1);
			return new Vector2D(x, y);
		}
	}
}
