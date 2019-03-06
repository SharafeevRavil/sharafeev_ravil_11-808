using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sem1 {
	class ConvexHull2 {
		public static Vector2[] Chan(LinkedList<Vector2> input, ref int count) {
			int t = 0;
			int[] tArr = new int[] {
				4,
				16,
				256,
				65536,
				int.MaxValue,
			};
			while (true) {
				int m = Math.Min(input.Count, tArr[t]);
				var ans = ChanWithParam(input, m, ref count);
				if (ans != null) {
					return ans;
				}
				t++;
			}
		}

		private static Vector2[] ChanWithParam(LinkedList<Vector2> input, int pointsInGroup, ref int count) {
			Stack<Vector2> hullStack = new Stack<Vector2>();
			Vector2[][] points = new Vector2[(int)Math.Ceiling(((double)input.Count) / pointsInGroup)][];
			var cur = input.First;
			for (int i = 0; i < points.GetLength(0); i++) {
				points[i] = new Vector2[((i + 1) * pointsInGroup < input.Count) ? pointsInGroup : input.Count - pointsInGroup * i];
				for (int j = 0; j < points[i].Length; j++) {
					points[i][j] = cur.Value;
					cur = cur.Next;
					count++;
				}
			}
			Vector2[][] hulls = new Vector2[points.GetLength(0)][];//FIXME: строим выпуклые оболочки грэхемом
			for (int i = 0; i < points.GetLength(0); i++) {
				hulls[i] = Graham(points[i], ref count);
			}
			if (hulls.GetLength(0) == 1) {
				return hulls[0];
			}

			Vector2 start = FindBotLeft(input);
			Vector2 current = start;
			do {
				if (hullStack.Count > pointsInGroup) {
					return null;
				}
				hullStack.Push(current);
				Vector2 next = FindTangent(hulls[0], current);
				for (int i = 1; i < points.GetLength(0); i++) {
					var qi = FindTangent(hulls[i], current);
					if (IsLeftTurn(current, qi, next)) {
						next = qi;
					}
					count++;
				}
				current = next;
			} while (current != start);
			return hullStack.ToArray();
		}

		public static Vector2 FindBotLeft(LinkedList<Vector2> a) {
			var cur = a.First;
			float x = cur.Value.X;
			float y = cur.Value.Y;
			Vector2 ans = cur.Value;
			while (cur.Next != null) {
				cur = cur.Next;
				if (cur.Value.Y < y || cur.Value.X < x && cur.Value.Y == y) {
					ans = cur.Value;
				}
			}
			return ans;
		}

		public static Vector2 FindBotLeft(Vector2[] a) {
			float x = a[0].X;
			float y = a[0].Y;
			Vector2 ans = a[0];
			for (int i = 1; i < a.Length; i++) {
				if (a[i].Y < y || a[i].X < x && a[i].Y == y) {
					ans = a[i];
				}
			}
			return ans;
		}

		static bool IsLeftTurn(Vector2 a, Vector2 b, Vector2 c) {
			return ((b.X - a.X) * (c.Y - b.Y) - (b.Y - a.Y) * (c.X - b.X) >= 0);
		}

		public static Vector2[] Graham(Vector2[] points, ref int count) {
			var start = FindBotLeft(points);
			Quicksort(points, 0, points.Length - 1, start, ref count);
			return GrahamArray(points, ref count).toArray();
		}

		public class MyStack<T> {//Самописный стэк на связных списках
			public class Element<T> {
				public T Value;

				public Element<T> Next;

				public Element(Element<T> next, T value) {
					Next = next;
					Value = value;
				}
			}

			public T[] toArray() {
				T[] ans = new T[Count];
				while (Count > 0) {
					ans[ans.Length - Count] = Pop();
				}
				return ans;
			}

			private Element<T> first;

			public int Count {
				get;
				private set;
			}

			public MyStack() {
				Clear();
			}

			public void Clear() {
				first = new Element<T>(null, default(T));
				Count = 0;
			}

			public void Push(T value) {
				first = new Element<T>(first, value);
				Count++;
			}

			public T Pop() {
				T ans = first.Value;
				first = first.Next;
				Count--;
				return ans;
			}

			public T Peek() {
				return first.Value;
			}

			public Element<T> PeekElement() {
				return first;
			}

			public T NextToTop() {
				if (first.Next == null) {
					return default(T);
				}
				return first.Next.Value;
			}
		}

		public static MyStack<Vector2> GrahamArray(Vector2[] sortedPoints, ref int count) {
			var stack = new MyStack<Vector2>();
			stack.Push(sortedPoints[0]);
			stack.Push(sortedPoints[1]);
			var j = 2;
			for (var i = 2; i < sortedPoints.Length; i++) {
				while (stack.NextToTop() != null && !IsLeftTurn(stack.NextToTop(), stack.Peek(), sortedPoints[i])) {
					stack.Pop();
				}

				stack.Push(sortedPoints[i]);
				j++;
				count++;
			}
			return stack;
		}

		static int Partition(Vector2[] m, int a, int b, Vector2 point, ref int count) {
			int i = a;
			for (int j = a; j <= b; j++) {
				if (Math.Atan2((m[j].Y - point.Y), (m[j].X - point.X)) < Math.Atan2((m[b].Y - point.Y), (m[b].X - point.X)) ||
				(Math.Atan2((m[j].Y - point.Y), (m[j].X - point.X)) == Math.Atan2((m[b].Y - point.Y), (m[b].X - point.X)) &&
				((point.X - m[j].X) * (point.X - m[j].X) + (point.Y - m[j].Y) * (point.Y - m[j].Y)) <= ((point.X - m[b].X) * (point.X - m[b].X) + (point.Y - m[b].Y) * (point.Y - m[b].Y)))) {
					Vector2 t = m[i];
					m[i] = m[j];
					m[j] = t;
					i++;
				}
				count++;
			}
			return i - 1;
		}

		public static void Quicksort(Vector2[] m, int a, int b, Vector2 mainPoint, ref int count) {
			if (a >= b) return;
			int c = Partition(m, a, b, mainPoint, ref count);
			Quicksort(m, a, c - 1, mainPoint, ref count);
			Quicksort(m, c + 1, b, mainPoint, ref count);
		}

		public static Vector2 FindTangent(Vector2[] points, Vector2 p) {
			int left = 0;
			int mid = 0;
			int right = points.Length;
			int prevTurn = 0;
			int nextTurn = 0;
			int midPrevTurn = 0;
			int midNextTurn = 0;
			int midSideTurn = 0;
			int size = points.Length;
			if (size - 1 >= 0) {
				prevTurn = orientation(p, points[0], points[size - 1]);
			}
			nextTurn = orientation(p, points[0], points[(left + 1) % right]);
			while (left < right) {
				mid = (left + right) / 2;
				if (points[mid] == p) {
					mid--;
					mid = mid % size;
				}
				if (((mid - 1) % size) >= 0)
					midPrevTurn = orientation(p, points[mid], points[(mid - 1) % size]);
				else
					midPrevTurn = orientation(p, points[mid], points[size - 1]);
				midNextTurn = orientation(p, points[mid], points[(mid + 1) % size]);
				midSideTurn = orientation(p, points[left], points[mid]);
				if (midPrevTurn != -1 && midNextTurn != -1) {
					return points[mid];
				}
				else if (midSideTurn == 1 && (nextTurn == -1 || prevTurn == nextTurn) || midSideTurn == -1 && midPrevTurn == -1) {
					right = mid;
				}
				else {
					left = mid + 1;
					prevTurn = -midNextTurn;
					if (left < size)
						nextTurn = orientation(p, points[left], points[(left + 1) % size]);
					else
						return points[0];
				}
			}
			return points[left];
		}

		static int compare(double a, double b) {
			if (a > b)
				return 1;
			else if (a < b)
				return -1;
			return 0;
		}

		static int orientation(Vector2 p, Vector2 q, Vector2 r) {
			return compare(((q.X - p.X) * (r.Y - p.Y)) - ((q.Y - p.Y) * (r.X - p.X)), 0);
		}

	}
}
