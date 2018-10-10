using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names {
	public class Application {
		//Синглтон
		private static Application instance;
		public static Application Instance {
			get {
				return instance;
			}
			set {
				if (instance == null) {
					instance = value;
				}
				else {
					//экземпляр существует
					throw new Exception("Aplication instance is already exist!");
				}
			}
		}
		//Путь до файла с данными
		public string DataFilePath = "names.txt";
		//Промежуток между строками
		public float MinGap = 0.0f;
		//Первый год первого промежутка
		public int FirstYear = 1936;
		//Число колонок по 12 лет
		public int ColumnCount = 5;

		public NameData[] NamesData;
		public Dictionary<string, double> CountByName;

		public Application() {
			if (Instance != null) {
				//экземпляр существует
				throw new Exception("Aplication instance is already exist!");
			}

			NamesData = Tools.ReadData(DataFilePath);
		}

		public void ShowHistogram(int count) {
			Charts.ShowHistogram(Tools.GetNameTop(Instance.NamesData, count));
		}
	}
}
