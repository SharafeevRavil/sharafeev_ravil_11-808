/*
 * Решается задача: https://ulearn.me/Course/BasicProgramming/Praktika_Bil_yard__e191b760-98cd-4d54-981a-971f0b4e319d?version=-1
 * 
 * Автор решения: Шарафеев Равиль, 11-808
 */

﻿using System;

namespace Billiards
{
	public static class BilliardsTask
	{
		public static double BounceWall(double directionRadians, double wallInclinationRadians)
		{
			return wallInclinationRadians*2-directionRadians;
		}
	}
}
