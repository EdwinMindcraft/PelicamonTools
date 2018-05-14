using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pelicamon.ModManager.API.Controls {
	public partial class VerticalSeparator : Control {

		public VerticalSeparator() {
			this.Paint += new PaintEventHandler(Separator_Paint);
			this.MaximumSize = new Size(2, 2000);
			this.MinimumSize = new Size(2, 0);
			this.Width = 350;
		}

		private void Separator_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(0, this.Height));
			g.DrawLine(Pens.White, new Point(1, 0), new Point(1, this.Height));
		}
	}
}
