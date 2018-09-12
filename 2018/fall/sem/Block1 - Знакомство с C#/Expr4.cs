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

<<<<<<< HEAD:2018/fall/sem/Block1 - Знакомство с C#/Expr4.cs
			Console.WriteLine( (int)(((double)n-0.5)/x) + (int)(((double)n - 0.5)/y) - (int)(((double)n - 0.5)/(SomeFunc(x, y)) );
			Console.ReadKey();
		}

		static int SomeFunc(int x, int y) {
=======
			Console.WriteLine((int)(((double)n - 0.5) / x) + (int)(((double)n - 0.5) / y) - (int)(((double)n - 0.5) / (Some(x, y))));
			Console.ReadKey();
		}

		static int Some(int x, int y) {
>>>>>>> 8d07a7b56ee5d429c3675e8fb9dfdd245bc282cd:2018/fall/sem/Expr4.cs
			if (x == y) {
				return x;
			}
			else {
				return x * y;
			}
		}
	}
}
