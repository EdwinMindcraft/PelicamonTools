using System.Drawing;
using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
	public partial class HorizontalSeparator : Control {
		public HorizontalSeparator() {
			this.Paint += new PaintEventHandler(Separator_Paint);
			this.MaximumSize = new Size(2000, 2);
			this.MinimumSize = new Size(0, 2);
			this.Width = 350;
		}

		private void Separator_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(this.Width, 0));
			g.DrawLine(Pens.White, new Point(0, 1), new Point(this.Width, 1));
		}
	}
}
