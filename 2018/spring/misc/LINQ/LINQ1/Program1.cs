using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15 {
	class Program1 {
		static void Main(string[] args) {
			IEnumerable<string> answer = a
				.Select((x, i) => x == "" ? "" : x + i)
				.Where(x => x != "")
				.OrderBy(x => x);
		}
	}
}
