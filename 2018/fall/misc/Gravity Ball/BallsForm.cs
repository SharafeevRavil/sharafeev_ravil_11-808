using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GravityBalls {
	public class BallsForm : Form {
		private Timer timer;
		private WorldModel world;

		private WorldModel CreateWorldModel() {
			var w = new WorldModel {
				WorldHeight = ClientSize.Height,
				WorldWidth = ClientSize.Width,
				BallRadius = 10,
				HoleRadius = 5
			};
			w.StartGame();
			return w;
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			world.WorldHeight = ClientSize.Height;
			world.WorldWidth = ClientSize.Width;
		}

		protected override void OnLoad(EventArgs e) {
			//Вызывается при загрузке
			base.OnLoad(e);
			DoubleBuffered = true;
			BackColor = Color.Yellow;
			world = CreateWorldModel();
			timer = new Timer { Interval = 30 };
			timer.Tick += TimerOnTick;
			timer.Start();
			world.WorldHeight = ClientSize.Height;
			world.WorldWidth = ClientSize.Width;
		}

		private void TimerOnTick(object sender, EventArgs eventArgs) {
			world.SimulateTimeframe(timer.Interval / 1000d);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.HighQuality;

			PaintBall(g);
			PaintMouse(g);
			foreach(Vector2D holePos in world.HolePositions) {
				PaintHole(g, holePos);
			}
		}

		private void PaintBall(Graphics g) {
			g.FillEllipse(Brushes.Black,
				(float)(world.BallPos.X - world.BallRadius),
				(float)(world.BallPos.Y - world.BallRadius),
				2 * (float)world.BallRadius,
				2 * (float)world.BallRadius);
		}

		private void PaintMouse(Graphics g) {
			double mouseRadius = 3;
			g.FillEllipse(Brushes.DeepSkyBlue,
				(float)(world.MousePos.X - mouseRadius),
				(float)(world.MousePos.Y - mouseRadius),
				2 * (float)mouseRadius,
				2 * (float)mouseRadius);
		}

		private void PaintHole(Graphics g, Vector2D holePos) {
			g.FillEllipse(Brushes.OrangeRed,
				(float)(holePos.X),
				(float)(holePos.Y),
				2 * (float)world.HoleRadius,
				2 * (float)world.HoleRadius);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			Text = string.Format("Cursor ({0}, {1})", e.X, e.Y);
			world.MousePos = new Vector2D(e.X, e.Y);
		}
	}
}