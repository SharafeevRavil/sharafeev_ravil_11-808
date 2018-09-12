using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr13 {
	class Program {
		static void Main(string[] args) {
			string[] input = Console.ReadLine().Split();

			int size = Int32.Parse(input[0]);
			int radius = Int32.Parse(input[1]);

			if (radius <= size / 2) {
				Console.WriteLine(radius * radius * Math.PI);
			}
			else if (radius < size / 2 * Math.Sqrt(2)) {
				double a = Math.Sqrt(radius * radius - size * size / 4.0);
				double angle = Math.Atan(a / size * 2) * 2;
				Console.WriteLine(radius * radius * Math.PI + 4 * ((a * size / 2) - (radius * radius * angle / 2)));
			}
			else {
				Console.WriteLine(size * size);
			}
			Console.ReadKey();
		}
	}
}
