using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife {
	class Program {
		public static void Paint(bool[,] field) {
			Console.SetCursorPosition(0, 0);
			for (int y = 0; y < field.GetLength(1); y++) {
				for (int x = 0; x < field.GetLength(0); x++) {
					var symbol = field[x, y] ? '#' : ' ';
					Console.Write(symbol);
				}
				Console.WriteLine();
			}
		}

		static void Main(string[] args) {						
			Random random = new Random();
			int xSize = 79;
			int ySize = 24;
			bool[,] field = new bool[xSize, ySize];
			for (int i = 0; i < xSize; i++) {
				for (int j = 0; j < ySize; j++) {
					field[i, j] = random.Next() % 2 == 0 ? false : true;
				}
			}
			
			StartGame(field);
		}

		public static void StartGame(bool[,] field, int maxIterations = int.MaxValue) {
			for (int i = 0; i < maxIterations; i++) {
				Paint(field);
				Thread.Sleep(100);
				field = Game.NextStep(field);
			}
		}
	}
}
