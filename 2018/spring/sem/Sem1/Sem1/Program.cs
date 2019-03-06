using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sem1 {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
			//mainForm.ShowDialog()
			/*Vector2[] vec = new Vector2[] {
				new Vector2(-7, -1),
				new Vector2(4, 0),
				new Vector2(9, -6),
				new Vector2(0, -14),
			};*/
			//GenerateRandom();

			//сравнение с грэхемом
			/*for (int i = 0; i < 100; i++) {
				string input = File.ReadAllText($"{i}.txt");
				string[] inputs = input.Split();
				Vector2[] vectors = new Vector2[input.Length / 2];
				for (int j = 0; j < inputs.Length; j += 2) {
					vectors[j / 2] = new Vector2(float.Parse(inputs[j]), float.Parse(inputs[j + 1]));
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ConvexHull.Chan(vectors);
				stopwatch.Stop();
				var chan = stopwatch.ElapsedMilliseconds;
				stopwatch.Reset();

				stopwatch.Start();
				Vector2 start = ConvexHull.FindBotLeft(vectors);
				ConvexHull.Quicksort(vectors, 0, vectors.Length - 1, start);
				ConvexHull.GrahamArray(vectors);
				stopwatch.Stop();
				var graham = stopwatch.ElapsedMilliseconds;
				MessageBox.Show($"chan = {chan} graham = {graham}");
			}*/

			//Vector2 start = ConvexHull.FindBotLeft(vec);
			/*
			ConvexHull.Quicksort(vec, 0, vec.Length - 1, start);
			var b = ConvexHull.GrahamArray(vec);
			var arr = ConvexHull.GrahamArray(vec).ToArray();*/
			/*
			Random rand = new Random();
			vec = new Vector2[rand.Next() % 10 + 5];
			for (int i = 0; i < vec.Length; i++) {
				vec[i] = new Vector2(rand.Next() % 21 - 10, rand.Next() % 21 - 10);
			}

			foreach (var a in ConvexHull.Chan(vec)) {
				MessageBox.Show(a.ToString());
			}*/
		}
	}
}
