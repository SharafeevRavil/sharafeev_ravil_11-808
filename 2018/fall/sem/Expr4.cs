using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr4 {
	class Program {
		static void Main(string[] args) {
			int n = Int32.Parse(Console.ReadLine());
				int x = Int32.Parse(Console.ReadLine());
			int y = Int32.Parse(Console.ReadLine());

			Console.WriteLine( (int)(((double)n-0.5)/x) + (int)(((double)n - 0.5)/y) - (int)(((double)n - 0.5)/(x*y)) );
			Console.ReadKey();
		}
	}
}
