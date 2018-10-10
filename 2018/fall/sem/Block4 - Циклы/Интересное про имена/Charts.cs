﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace Names {
	internal static class Charts {
		public static void ShowHistogram(HistogramData stats) {
			var chart = new ZedGraphControl() {
				Dock = DockStyle.Fill
			};

			//Добавление собственной формы с добавленным слушателем нажатий
			var form = new MyForm() {
				Text = stats.Title,
				Size = new Size(800, 600)
			};
			chart.MouseDownEvent += new ZedGraphControl.ZedMouseEventHandler(form.ZedGraph_MouseDownEvent);
			//Имя гистограммы
			chart.GraphPane.Title.Text = stats.Title;
			//Разрешение скролла гистограммы
			chart.IsShowHScrollBar = true;
			chart.IsShowVScrollBar = true;
			chart.IsAutoScrollRange = true;
			//Добавление строк соответственно HistogramData
			chart.GraphPane.BarSettings.Base = BarBase.Y;
			chart.GraphPane.AddBar(
				"People count", 
				stats.YValues, 
				Enumerable.Range(0, stats.YValues.Length)
					.Select(i => (double)i).ToArray(),
				Color.DarkOrange
			);
			chart.GraphPane.YAxis.Scale.MaxAuto = true;
			chart.GraphPane.YAxis.Scale.MinAuto = true;
			//Добавление отступа между строками
			chart.GraphPane.BarSettings.MinClusterGap = Application.Instance.MinGap;
			//Добавление имен
			chart.GraphPane.YAxis.Scale.TextLabels = stats.XLabels;
			chart.GraphPane.YAxis.Type = AxisType.Text;
			//Применение изменений и отображение гистограммы
			chart.AxisChange();
			form.Controls.Add(chart);
			form.ShowDialog();
		}

		//Отображение Heatmap осталось прежним
		public static void ShowHeatmap(HeatmapData stats) {
			var form = new Form {
				Text = stats.Title,
				Size = new Size(800, 600)
			};
			form.Paint += (s, e) => DrawHeatmap(form.ClientRectangle, e.Graphics, stats);
			form.ShowDialog();
		}

		private static void DrawHeatmap(Rectangle clientRect, Graphics g, HeatmapData data) {
			var values = data.Heat.Cast<double>().ToList();
			var avgHeat = values.Average();
			var sigma = Math.Sqrt(values.Average(x => (x - avgHeat) * (x - avgHeat)));
			var cellWidth = clientRect.Width / (data.XLabels.Length + 1);
			var cellHeight = clientRect.Height / (data.YLabels.Length + 1);
			for (int x = 0; x < data.XLabels.Length; x++)
				for (int y = 0; y < data.YLabels.Length; y++) {
					var color = GetColor(data.Heat[x, y], avgHeat, sigma);
					var cellRect = new Rectangle(
						clientRect.Left + cellWidth * (1 + x),
						clientRect.Top + cellHeight * y,
						cellWidth,
						cellHeight
						);
					g.FillRectangle(new SolidBrush(color), cellRect);
				}
			DrawLabels(g, data, cellWidth, cellHeight);
		}

		private static void DrawLabels(Graphics g, HeatmapData data, int cellWidth, int cellHeight) {
			var font = new Font(FontFamily.GenericMonospace, 10);
			for (int x = 0; x < data.XLabels.Length; x++) {
				var text = data.XLabels[x];
				var labelRect = new RectangleF(cellWidth * (1 + x), data.YLabels.Length * cellHeight, cellWidth, cellHeight);
				var format = new StringFormat {
					LineAlignment = StringAlignment.Near,
					Alignment = StringAlignment.Center
				};
				g.DrawString(text, font, new SolidBrush(Color.Black), labelRect, format);
			}
			for (int y = 0; y < data.YLabels.Length; y++) {
				var text = data.YLabels[y];
				var labelRect = new RectangleF(0, y * cellHeight, cellWidth, cellHeight);
				var format = new StringFormat {
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Far
				};
				g.DrawString(text, font, new SolidBrush(Color.Black), labelRect, format);
			}
		}


		private static Color GetColor(double value, double avgHeat, double sigma) {
			var sigmaValue = (value - avgHeat) / sigma;
			var colorValue = Math.Min(255, (int)(200 * Math.Abs(sigmaValue)));
			var color = sigmaValue >= 0
				? Color.FromArgb(255, 255 - colorValue, 255, 255 - colorValue)
				: Color.FromArgb(255, 255, 255 - colorValue, 255 - colorValue);
			return color;
		}
	}
}