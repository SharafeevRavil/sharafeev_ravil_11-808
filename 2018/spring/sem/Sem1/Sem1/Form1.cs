using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Sem1 {
	public partial class Form1 : Form {
		static void GenerateRandom() {
			Random random = new Random();
			for (int i = 0; i < 100; i++) {
				StringBuilder input = new StringBuilder();
				for (int j = 0; j < (i + 1) * 100; j++) {
					input.Append($"{(j > 0 ? " " : "")}{((float)random.NextDouble() - 0.5f) * 2 * 100} {((float)random.NextDouble() - 0.5f) * 2 * 100}");
				}
				File.WriteAllText($"{i}.txt", input.ToString());
			}
		}

		static void GenerateKiller() {
			Random random = new Random();
			float a = 4;
			float b = 9;
			for (int i = 0; i < 100; i++) {
				StringBuilder input = new StringBuilder();
				for (int j = 0; j < (i + 1) * 100; j++) {
					float x = ((float)random.NextDouble() - 0.5f) * 2 * 2;
					float y = (float)Math.Sqrt(36 - 9 * x * x) / 2 * (random.NextDouble() > 0.5 ? -1 : 1);
					input.Append($"{(j > 0 ? " " : "")}{x * 1000} {y * 1000}");
				}
				File.WriteAllText($"{i}k.txt", input.ToString());
			}
		}

		public Form1() {
			InitializeComponent();

			GraphPane pane = zedGraphControl1.GraphPane;
			pane.CurveList.Clear();

			PointPairList list = new PointPairList();
			PointPairList list2 = new PointPairList();
			PointPairList list3 = new PointPairList();
			PointPairList list23 = new PointPairList();
			
			GenerateRandom();
			for (int i = 0; i < 100; i++) {
				string input = File.ReadAllText($"{i}.txt");
				string[] inputs = input.Split();
				Vector2[] vectors = new Vector2[inputs.Length / 2];
				for (int j = 0; j < inputs.Length; j += 2) {
					vectors[j / 2] = new Vector2(float.Parse(inputs[j]), float.Parse(inputs[j + 1]));
				}
				Stopwatch stopwatch = new Stopwatch();
				int count = 0;
				stopwatch.Start();
				//ConvexHull.Chan(vectors, ref count);
				ConvexHull2.Chan(new LinkedList<Vector2>(vectors), ref count);
				stopwatch.Stop();
				list.Add(inputs.Length / 2, stopwatch.ElapsedMilliseconds*1000);
				list2.Add(inputs.Length / 2, count);
				stopwatch.Reset();
			}

			LineItem myCurve = pane.AddCurve("chanMilliseconds * 1k", list, Color.Blue, SymbolType.None);

			LineItem myCurve2 = pane.AddCurve("chanIterations", list2, Color.Red, SymbolType.Diamond);


			/*
			GenerateKiller();
			GenerateRandom();
			for (int i = 0; i < 100; i++) {
				string input = File.ReadAllText($"{i}.txt");
				string[] inputs = input.Split();
				Vector2[] vectors = new Vector2[inputs.Length / 2];
				for (int j = 0; j < inputs.Length; j += 2) {
					vectors[j / 2] = new Vector2(float.Parse(inputs[j]), float.Parse(inputs[j + 1]));
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				DateTime time = DateTime.Now;
				int count = 0;
				ConvexHull.Chan(vectors, ref count);
				stopwatch.Stop();
				list.Add(inputs.Length / 2, stopwatch.ElapsedMilliseconds);
				list3.Add(inputs.Length / 2, (DateTime.Now - time).Milliseconds);
				stopwatch.Reset();
			}

			for (int i = 0; i < 100; i++) {
				string input = File.ReadAllText($"{i}k.txt");
				string[] inputs = input.Split();
				Vector2[] vectors = new Vector2[inputs.Length / 2];
				for (int j = 0; j < inputs.Length; j += 2) {
					vectors[j / 2] = new Vector2(float.Parse(inputs[j]), float.Parse(inputs[j + 1]));
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				DateTime time = DateTime.Now;
				int count = 0;
				ConvexHull.Chan(vectors, ref count);
				stopwatch.Stop();
				list2.Add(inputs.Length / 2, stopwatch.ElapsedMilliseconds);
				list23.Add(inputs.Length / 2, (DateTime.Now - time).Milliseconds);
				stopwatch.Reset();
			}

			LineItem myCurve = pane.AddCurve("chan", list, Color.Blue, SymbolType.None);
			LineItem myCurve3 = pane.AddCurve("chan2", list3, Color.Black, SymbolType.None);

			LineItem myCurve2 = pane.AddCurve("chanKiller", list2, Color.Red, SymbolType.Diamond);
			LineItem myCurve23 = pane.AddCurve("chanKiller2", list23, Color.DarkOrange, SymbolType.Diamond);
			*/


			/*
			GenerateRandom();
			for (int i = 0; i < 100; i++) {
				string input = File.ReadAllText($"{i}.txt");
				string[] inputs = input.Split();
				Vector2[] vectors = new Vector2[inputs.Length / 2];
				for (int j = 0; j < inputs.Length; j += 2) {
					vectors[j / 2] = new Vector2(float.Parse(inputs[j]), float.Parse(inputs[j + 1]));
				}
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				DateTime time = DateTime.Now;
				int count = 0;
				ConvexHull.Chan(vectors, ref count);
				stopwatch.Stop();
				list.Add(inputs.Length / 2, stopwatch.ElapsedMilliseconds);
				list3.Add(inputs.Length / 2, (DateTime.Now - time).Milliseconds);
				stopwatch.Reset();

				stopwatch = new Stopwatch();
				stopwatch.Start();
				time = DateTime.Now;
				ConvexHull2.Chan(new LinkedList<Vector2>(vectors));
				stopwatch.Stop();
				list2.Add(inputs.Length / 2, stopwatch.ElapsedMilliseconds);
				list23.Add(inputs.Length / 2, (DateTime.Now - time).Milliseconds);
				stopwatch.Reset();
			}

			LineItem myCurve = pane.AddCurve("chan", list, Color.Blue, SymbolType.None);
			LineItem myCurve3 = pane.AddCurve("chan2", list3, Color.Black, SymbolType.None);

			LineItem myCurve2 = pane.AddCurve("chanList", list2, Color.Red, SymbolType.Diamond);
			LineItem myCurve23 = pane.AddCurve("chanList2", list23, Color.DarkOrange, SymbolType.Diamond);
			*/

			zedGraphControl1.AxisChange();
			zedGraphControl1.Invalidate();
		}
	}
}
