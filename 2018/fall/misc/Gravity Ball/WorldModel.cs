using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GravityBalls {
	public class WorldModel {
		public double BallRadius;
		public double HoleRadius;
		public double WorldWidth;
		public double WorldHeight;

		public Vector2D BallPos;
		public Vector2D MousePos;

		public List<Vector2D> HolePositions = new List<Vector2D>();
		public List<Vector2D> HoleSpeeds = new List<Vector2D>();

		public Random Rand = new Random();

		public double BallSpeed = 50;

		public Vector2D Speed;
		public Vector2D G = new Vector2D(0, 9.81);

		public double BounceKoef = 0.1; //Коэффициент потерь при отскоке
		public double AirKoef = 0.05; //Сопротивление воздуха(теряется за секунду)
		public double TimeKoef = 3;
		public double MaxMouseSpeed = 100;
		public double MaxDistance = 100;

		public double MinHoleDist = 20;
		public double HoleSpeed = 30;

		public double lastHoleTime = 0;
		public double holeCreationTime = 2;

		public bool isLosed = false;

		public void SimulateTimeframe(double dt) {
			if (isLosed) {
				return;
			}

			Move(dt);
			MoveHoles(dt);
			CheckHoleTouch();

			lastHoleTime += dt;
			if (lastHoleTime >= holeCreationTime) {
				CreateHole(MinHoleDist, HoleSpeed);
				lastHoleTime = 0;
			}
		}

		public void CheckHoleTouch() {
			foreach (Vector2D holePos in HolePositions) {
				if ((holePos - BallPos).Length <= BallRadius + HoleRadius && !isLosed) {
					isLosed = true;
					DialogResult dialogResult = MessageBox.Show("You lose\nPlay again?", "Defeat", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes) {
						StartGame();
						isLosed = false;
						break;
					}
					else if (dialogResult == DialogResult.No) {
						Application.Exit();
					}
				}
			}
		}

		public void StartGame() {
			BallPos = new Vector2D(WorldWidth / 2, WorldHeight / 2);
			Speed = Vector2D.Random(Rand) * BallSpeed;
			MousePos = new Vector2D(Control.MousePosition.X, Control.MousePosition.Y);
			HoleSpeeds.Clear();
			HolePositions.Clear();
		}

		public void Move(double dt) {
			//Изменение скорости под действием ускорения
			AddForce(G, 1, dt);
			Speed.Scale(1 - AirKoef * dt);
			Vector2D mouseVector = BallPos - MousePos;
			if (/*mouseVector.Length < 8000 && */mouseVector.Length < MaxDistance) {
				AddForce(mouseVector.Normalize(), MaxMouseSpeed * (1 - mouseVector.Length / MaxDistance), dt);
			}

			//Движение под действием скорости
			BallPos.X = Clamp(BallPos.X + TimeKoef * dt * Speed.X, BallRadius, WorldWidth - BallRadius);
			BallPos.Y = Clamp(BallPos.Y + TimeKoef * dt * Speed.Y, BallRadius, WorldHeight - BallRadius);
			//Проверка на касание со стеной
			CheckBounce(BallPos, Speed, BounceKoef, BallRadius);
		}

		private void AddForce(Vector2D force, double koef, double dt) {
			Speed += force * koef * dt;
		}

		private double Clamp(double value, double min, double max) {
			return value < min ? min : value > max ? max : value;
		}

		private void CheckBounce(Vector2D pos,Vector2D speed, double koef, double radius) {
			//Левая стенка
			if (pos.X - radius < double.Epsilon) {
				speed.Bounce(Vector2D.Direction.Vertical, koef);
			}
			//Правая стенка
			if (WorldWidth - radius - pos.X < double.Epsilon) {
				speed.Bounce(Vector2D.Direction.Vertical, koef);
			}
			//Нижняя стенка
			if (pos.Y - radius < double.Epsilon) {
				speed.Bounce(Vector2D.Direction.Horizontal, koef);
			}
			//Верхняя стенка
			if (WorldHeight - radius - pos.Y < double.Epsilon) {
				speed.Bounce(Vector2D.Direction.Horizontal, koef);
			}
		}

		public void CreateHole(double minDist, double speed) {
			Vector2D holePos = new Vector2D(HoleRadius + Rand.Next() % WorldWidth - 2 * HoleRadius, HoleRadius + Rand.Next() % WorldHeight - 2 * HoleRadius);
			while ((holePos - BallPos).Length < BallRadius + HoleRadius + minDist) {
				holePos = new Vector2D(HoleRadius + Rand.Next() % WorldWidth - 2 * HoleRadius, HoleRadius + Rand.Next() % WorldHeight - 2 * HoleRadius);
			}
			HoleSpeeds.Add(Vector2D.Random(Rand) * speed);
			HolePositions.Add(holePos);
		}

		public void MoveHoles(double dt) {
			for (int i = 0; i < HolePositions.Count; i++) {
				HolePositions[i].X = Clamp(HolePositions[i].X + TimeKoef * dt * HoleSpeeds[i].X, HoleRadius, WorldWidth - HoleRadius);
				HolePositions[i].Y = Clamp(HolePositions[i].Y + TimeKoef * dt * HoleSpeeds[i].Y, HoleRadius, WorldHeight - HoleRadius);
				CheckBounce(HolePositions[i], HoleSpeeds[i], 0, HoleRadius);
			}
		}
	}
}