using MapBuilder.Tiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapBuilder.Controls {
	public partial class TilesetPalette : UserControl {

		public Tileset Tileset {
			get {
				return tileset;
			}
			set {
				tileset = value;
				Selected = -1;
				if (tileset != null) {
					tileset.TileUpdated += this.Tileset_TileUpdated;
				}
			}
		}

		private void Tileset_TileUpdated(object sender, EventArgs e) {
			this.vScrollBar1.Minimum = 0;
			int i = tileset.Tiles.Count - 1;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			this.vScrollBar1.Maximum = y + 1;
			this.panel1.VerticalScroll.Maximum = (y + 1) * RenderSize;
			this.panel1.VerticalScroll.Minimum = 0;
			this.panel1.VerticalScroll.Enabled = true;
			this.panel1.VerticalScroll.Visible = false;
			this.panel1.Size = new Size(panel1.Size.Width, y * RenderSize);
			panel1.Invalidate();
		}

		public delegate void TileSelectEvent(int id);
		public event TileSelectEvent OnTileSelect = new TileSelectEvent((id) => { });

		public int RenderSize { get; set; } = 64;
		public int DisplayWidth { get { return (int)Math.Floor((float)panel1.Width / RenderSize); } }
		private int yOffset = 0;
		private Tileset tileset;
		public int Selected { get; set; } = -1;

		public TilesetPalette() {
			InitializeComponent();
		}

		private void TilesetPalette_Paint(object sender, PaintEventArgs e) {
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			this.vScrollBar1.Value = e.NewValue;
			this.yOffset = e.NewValue;
			panel1.Location = new Point(panel1.Location.X, -this.yOffset * RenderSize);
		}

		private void panel1_MouseWheel(object sender, MouseEventArgs eventArgs) {
			int newVal = this.vScrollBar1.Value - eventArgs.Delta / 120;
			if (newVal < 0)
				newVal = 0;
			if (newVal > this.vScrollBar1.Maximum)
				newVal = this.vScrollBar1.Maximum;
			vScrollBar1_Scroll(panel1, new ScrollEventArgs(ScrollEventType.LargeDecrement, newVal));
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			if (tileset != null) {
				for (int i = 0; i < Tileset.Tiles.Count; i++) {
					int x = i % DisplayWidth;
					int y = (i - x) / DisplayWidth;
					x *= RenderSize;
					y *= RenderSize;
					Image tile = Tileset.Tiles[i];
					e.Graphics.DrawImage(tile, new Rectangle(x, y, RenderSize, RenderSize));
				}
				if (Selected != -1) {
					e.Graphics.DrawRectangle(Pens.White, new Rectangle((Selected % DisplayWidth) * RenderSize, ((Selected - (Selected % DisplayWidth)) / DisplayWidth) * RenderSize, RenderSize - 1, RenderSize - 1));
				}
			}
		}

		private void panel1_MouseClick(object sender, MouseEventArgs e) {
			int x = (int)Math.Floor((float)e.X / RenderSize);
			int y = (int)Math.Floor((float)e.Y / RenderSize);
			Selected = y * DisplayWidth + x;
			panel1.Invalidate();
		}
	}
}
