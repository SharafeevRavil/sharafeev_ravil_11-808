using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr12 {
	class Program {
		static void Main(string[] args) {
			string[] input = Console.ReadLine().Split();

			int h = Int32.Parse(input[0]); //height
			int t = Int32.Parse(input[1]); //time
			int v = Int32.Parse(input[2]); //maxSpeed
			int x = Int32.Parse(input[3]); //earsSpeed

			double min = (h - t * x < 0) ? 0 : (float)(h - t * x) / (v - x);
			double max = Math.Min(t, h/x);

			Console.WriteLine(min + " " + max);
			Console.ReadKey();
		}
	}
}
