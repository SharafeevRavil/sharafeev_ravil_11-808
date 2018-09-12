using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expr11 {
	class Program {
		static void Main(string[] args) {
			int hour = Int32.Parse(Console.ReadLine());
			int minute = Int32.Parse(Console.ReadLine());

			double hourDegrees = (hour % 12) * 30;
			double minDegrees = (minute % 60) * 6;
			hourDegrees += minute / 2;

			if(hourDegrees > minDegrees) {
				minDegrees += 360;
			}

			Console.WriteLine($"Angle is equal to {minDegrees - hourDegrees} degrees");
			Console.ReadKey();
		}
	}
}
