/*
 * Решается задача: AngryBirds
 * https://ulearn.me/Course/BasicProgramming/Praktika_Angry_Birds__9fdf2523-85d0-4180-8ef5-88ab8d6b21ff
 * Автор решения: Шарафеев Равиль, 11-808
 */


﻿using System;

namespace AngryBirds {
	public static class AngryBirdsTask {
		public static double FindSightAngle(double v, double distance) {
			return Math.Asin(distance * 9.8d / (v * v)) / 2;
		}
	}
}
