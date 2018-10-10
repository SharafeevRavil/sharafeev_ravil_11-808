using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Names
{
	public class Program
	{

		private static void Main(string[] args)
		{
			//Создание экземпляра класса Application, в котором находятся все необходимые
			//переменные настроек приложения и метод начала программы(ShowHistogram)
			Application.Instance = new Application();

			Console.WriteLine("Enter names count");
			int topCount = int.Parse(Console.ReadLine());
			Application.Instance.ShowHistogram(topCount);
			Console.ReadKey();
		}
	}
}