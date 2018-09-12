using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program {
	static void Main(string[] args) {
		Console.Write(Calculate(Console.ReadLine()));
		Console.ReadKey();
	}

	public static double Calculate(string userInput) {
		string[] inputs = userInput.Split();

		double summ = double.Parse(inputs[0]);
		double percent = double.Parse(inputs[1]) / 1200 + 1;
		int months = Int32.Parse(inputs[2]);
	
		return summ * Math.Pow(percent, months);
	}
}