using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names {
	//Класс с различными методами-инструментами, необходимыми для решения задачи
	public static class Tools {
		//Получение гистограммы из count столбцов, где count самых популярных имен
		public static HistogramData GetNameTop(NameData[] names, int count) {
			Dictionary<string, double> countByName = new Dictionary<string, double>();

			foreach (NameData nameData in names) {
				if (!countByName.ContainsKey(nameData.Name)) {
					countByName.Add(nameData.Name, 1);
				}
				else {
					countByName[nameData.Name]++;
				}
			}

			Application.Instance.CountByName = countByName
				.OrderByDescending(x => x.Value)
				.Take(count)
				.Reverse()
				.ToDictionary(x => x.Key, i => i.Value);

			//Заполнение массива строк названий дней для передачи в конструктор
			string[] labels = Application.Instance.CountByName
				.Select(x => x.Key)
				.ToArray();
			double[] namesCount = Application.Instance.CountByName
				.Select(x => x.Value)
				.ToArray();
			return new HistogramData($"Топ {count} имен", labels, namesCount);
		}
		//Получение тепловой карты годов рождаемости людей с именем name с firstYear по firstYear + 12 * columnCount года
		public static HeatmapData GetBirthsPerYearHeatmap(NameData[] names, string name, int firstYear, int columnCount) {
			double[,] years = new double[columnCount, 12];
			for (int i = 0; i < names.Length; i++) {
				//Если день рождения - первый день месяца или имя не совпадает
				if (names[i].BirthDate.Day == 1 || names[i].Name != name) {
					continue;
				}
				//Если год рождения за необходимыми пределами
				int year = names[i].BirthDate.Year - firstYear;
				if (year < 0 || year / 12 >= columnCount) {
					continue;
				}
				years[year / 12, year % 12]++;
			}

			string[] xLabel = new string[columnCount];
			for (int i = 0; i < columnCount; i++) {
				xLabel[i] = $"{firstYear + i * 12}-{firstYear + (i + 1) * 12 - 1}";
			}
			string[] yLabel = new string[12];
			for (int i = 0; i < 12; i++) {
				yLabel[i] = (i + 1).ToString();
			}

			return new HeatmapData(name, years, xLabel, yLabel);
		}
		//Получение имени, соответствующего строке по нажатым координатам
		public static string GetRowName(double x, double y, Dictionary<string, double> countByName) {
			y -= 0.5;
			int index = (int)(y / (1 + Application.Instance.MinGap));
			if (y > 0 && y - index * (1 + Application.Instance.MinGap) > Application.Instance.MinGap) {
				if (x > 0 && index < countByName.Keys.Count && countByName[countByName.Keys.ElementAt(index)] >= x) {
					return countByName.Keys.ElementAt(index);
				}
				return null;
			}
			return null;
		}
		//Чтение данных из файла по пути
		public static NameData[] ReadData(string dataFilePath) {
			return File
				.ReadLines(dataFilePath)
				.Select(NameData.ParseFrom)
				.ToArray();
		}
	}
}
