/*
 * Решается задача: 
 * https://ulearn.me/Course/BasicProgramming/Praktika_Dva_pryamougol_nika__1da7338b-910e-45af-9648-dcf615bf2f1c
 * Автор решения: Шарафеев Равиль, 11-808
 */

using System;

namespace Rectangles {
	public static class RectanglesTask {
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2) {
			//Если верхний отрезок одного из прямоугольников ниже нижнего отрезка другого прямоугольника или
			//если правый отрезок одного из прямоугольников левее левого отрезка другого прямоугольника
			//то прямоугольники не пересекаются
			if (r1.Top > r2.Bottom || r2.Top > r1.Bottom || r1.Left > r2.Right || r2.Left > r1.Right) {
				return false;
			}
			return true;
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2) {
			//Если прямые не пересекаются, то площадь равна 0
			if (!AreIntersected(r1, r2)) {
				return 0;
			}

			//Массив, в котором содержатся x всех параллельных оси ординат сторон прямоугольника
			int[] x = new int[]{
				r1.Left, r1.Right, r2.Left, r2.Right,
			};
			Array.Sort(x);

			//Массив, в котором содержатся y всех параллельных оси абцисс сторон прямоугольника
			int[] y = new int[]{
				r1.Top, r1.Bottom, r2.Top, r2.Bottom,
			};
			Array.Sort(y);

			//Если прямоугольники пересекаются, то область их пересечения находится между 
			//второй и третьей прямыми, параллельными оси ординат по x и
			//второй и третьей прямыми, параллельными оси абцисс по y
			return (x[2] - x[1]) * (y[2] - y[1]);
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2) {
			//Проверка того, что ЛЕВАЯ сторона одного прямоугольника находится ПРАВЕЕ ЛЕВОЙ стороны другого и т.д.
			if (r1.Left >= r2.Left && r1.Right <= r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom) {
				return 0;
			}
			else if (r2.Left >= r1.Left && r2.Right <= r1.Right && r2.Top >= r1.Top && r2.Bottom <= r1.Bottom) {
				return 1;
			}
			else {
				return -1;
			}
		}
	}
}