using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr5 {
	class Program {
		static void Main(string[] args) {
			int a = Int32.Parse(Console.ReadLine());
			int b = Int32.Parse(Console.ReadLine());

			Console.WriteLine(GetLeapYears(b) - GetLeapYears(a) + (CheckLeap(a) ? 1 : 0) );
			Console.ReadKey();
		}

		static int GetLeapYears(int year) {
			return year / 4 + year / 400 - year / 100;
		}

		static bool CheckLeap(int year) {
			return year % 400 == 0 ? true : year % 100 == 0 ? false : year % 4 == 0 ? true : false;
		}
	}
}
