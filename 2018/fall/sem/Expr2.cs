using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr2 {
	class Program {
		static void Main(string[] args) {
			int a;
			a = Int32.Parse(Console.ReadLine());

			a = a % 10 * 100 + a / 10 % 10 * 10 + a / 100;
			Console.WriteLine(a);
			Console.ReadKey();
		}
	}
}
