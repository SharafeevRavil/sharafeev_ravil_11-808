using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Names {
	public class MyForm : Form {
		public bool ZedGraph_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e) {
			// Пересчитать координаты из системы координат, связанной с контролом zedGraph 
			// в систему координат, связанную с графиком
			sender.GraphPane.ReverseTransform(new PointF(e.X, e.Y), out double graphX, out double graphY);
			//Получаю имя, которому соответствует нажатый столбец
			string name = Tools.GetRowName(graphX, graphY, Application.Instance.CountByName);
			if (name != null) {
				//Если нажатие было по столбцу, а не по пустому месту, вызываю отрисовку heatmap
				Charts.ShowHeatmap(Tools.GetBirthsPerYearHeatmap(
					Application.Instance.NamesData,
					name,
					Application.Instance.FirstYear,
					Application.Instance.ColumnCount
				));
			}
			return true;
		}
	}
}
