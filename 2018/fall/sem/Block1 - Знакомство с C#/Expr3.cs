using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr3 {
	class Program {
		static void Main(string[] args) {
			int h;
			h = Int32.Parse(Console.ReadLine());

			int angle = h % 12 * 30;

			Console.WriteLine(Math.Min(angle, 360 - angle));
			Console.ReadKey();
		}
	}
}
