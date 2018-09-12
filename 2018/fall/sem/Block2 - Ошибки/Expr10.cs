using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr10 {
	class Program {
		static void Main(string[] args) {
			int n = 1000;
			int k1 = 3;
			int k2 = 5;

			int ans = Some(n, k1) + Some(n, k2) - Some(n, k1 * k2);
			Console.WriteLine(ans);
			Console.ReadKey();
		}

		private static int Some(int n, int k) {
			return ((n - 1) / k * k + k) * (((n - 1) / k - 1) + 1) / 2;
		}
	}
}
