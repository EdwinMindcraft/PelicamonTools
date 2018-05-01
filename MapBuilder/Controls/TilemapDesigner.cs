using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapBuilder.Tiles;

namespace MapBuilder.Controls {
	public partial class TilemapDesigner : UserControl {

		public int RenderSize { get; set; } = 32;
		public Tilemap Tilemap { get; } = new Tilemap(20, 20);
		public Tileset Tileset { get; set; }

		public TilemapDesigner() {
			InitializeComponent();
			this.VerticalScroll.Visible = true;
			this.VerticalScroll.Enabled = false;
			this.VerticalScroll.Minimum = 0;
			this.HorizontalScroll.Visible = true;
			this.HorizontalScroll.Enabled = false;
			this.HorizontalScroll.Minimum = 0;
			Tilemap.TilemapUpdated += this.Tilemap_TilemapUpdated;
		}

		private void Tilemap_TilemapUpdated(object sender, EventArgs e) {
			this.panel1.Size = new Size(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height);
			int w = this.panel1.Width - this.Width + 20;
			int h = this.panel1.Height - this.Height + 20;

			this.HorizontalScroll.Value = 0;
			this.HorizontalScroll.Maximum = w > 0 ? w : 0;
			this.HorizontalScroll.Enabled = w > 0;
			this.HorizontalScroll.Visible = w > 0;

			this.VerticalScroll.Value = 0;
			this.VerticalScroll.Maximum = h > 0 ? h : 0;
			this.VerticalScroll.Enabled = h > 0;
			this.VerticalScroll.Visible = h > 0;
			this.Invalidate();
		}

		private void Panel_MouseClick(object sender, MouseEventArgs e) {
			int x = e.X - (e.X % RenderSize) / RenderSize;
			int y = e.Y - (e.Y % RenderSize) / RenderSize;
		}

		private void TilemapDesigner_Scroll(object sender, ScrollEventArgs e) {
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			for (int i = 0; i < panel1.Width; i += RenderSize / 2) {
				for (int j = 0; j < panel1.Height; j += RenderSize / 2) {
					int k = (i * 2) / RenderSize + (j * 2) / RenderSize;
					e.Graphics.FillRectangle(k % 2 == 1 ? Brushes.LightGray : Brushes.DarkGray, new Rectangle(i, j, RenderSize / 2, RenderSize / 2));
				}
			}
		}
	}
}
