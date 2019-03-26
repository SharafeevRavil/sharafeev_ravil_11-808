using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15 {
	public static class Extenshion {
		public static IEnumerable<Tuple<TItem, TItem>> Method<TItem>(this IEnumerable<TItem> first, IEnumerable<TItem> second, Func<TItem, TItem, bool> f) {
			foreach(var a in first) {
				foreach(var b in second) {
					if(f(a, b)) {
						yield return Tuple.Create(a, b);
					}
				}
			}
		}
	}

	class Program {
		static void Main(string[] args) {
		}
	}
}
