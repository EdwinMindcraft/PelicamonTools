using System;
using MapBuilder.Tiles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using MapBuilder.Tiles;

namespace MapBuilder.Controls {
	public partial class TilemapDesigner : UserControl {

		public int RenderSize { get; set; } = 32;
		public Tilemap Tilemap { get; } = new Tilemap(21, 15);
		public Tileset Tileset { get; set; }
		public int Selected { get; set; }
		private Bitmap background;

		public TilemapDesigner() {
			InitializeComponent();
			GenerateBackground();
			Tilemap.TilemapUpdated += this.Tilemap_TilemapUpdated;
		}

        public void TileSelectEventTrigerred(int id, Tileset sender)
        {
            Console.Out.WriteLine("FROM EVENT : " + id.ToString());
        }

        public TilesetPalette.TileSelectEvent TileSelectEventHandler()
        {
            return TileSelectEventTrigerred;
        }

        private void Tilemap_TilemapUpdated(object sender, EventArgs e) {
			this.panel1.Size = new Size(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height);
			int w = this.panel1.Width - this.Width + 20;
			int h = this.panel1.Height - this.Height + 20;

			this.hScrollBar1.Minimum = 0;
			this.hScrollBar1.Maximum = w > 0 ? w : 0;
			this.hScrollBar1.Enabled = w > 0;

			this.vScrollBar1.Value = 0;
			this.vScrollBar1.Minimum = 0;
			this.vScrollBar1.Maximum = h > 0 ? h : 0;
			this.vScrollBar1.Enabled = h > 0;
			GenerateBackground();
		}

		private void GenerateBackground() {
			background = new Bitmap(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height);
			Graphics g = Graphics.FromImage(background);
			for (int i = 0; i < panel1.Width; i += RenderSize / 2) {
				for (int j = 0; j < panel1.Height; j += RenderSize / 2) {
					int k = (i * 2) / RenderSize + (j * 2) / RenderSize;
					g.FillRectangle(k % 2 == 1 ? Brushes.LightGray : Brushes.DarkGray, new Rectangle(i, j, RenderSize / 2, RenderSize / 2));
				}
			}
			g.Dispose();
		}

		private void Panel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e) {
			int x = e.X - (e.X % RenderSize) / RenderSize;
			int y = e.Y - (e.Y % RenderSize) / RenderSize;
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(background, 0, 0);
		}


		private void TilemapDesigner_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e) {
			int delta = -e.Delta / 120;
			ScrollOrientation orientation = ScrollOrientation.VerticalScroll;
			int old = this.vScrollBar1.Value;
			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) {
				orientation = ScrollOrientation.HorizontalScroll;
				old = this.hScrollBar1.Value;
			}
			this.TilemapDesigner_Scroll(sender, new ScrollEventArgs(delta > 0 ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement, old, old + delta, orientation));
		}

		private void TilemapDesigner_Scroll(object sender, ScrollEventArgs e) {
			if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll || sender == hScrollBar1)
				this.hScrollBar1.Value = Math.Max(Math.Min(e.NewValue, this.hScrollBar1.Maximum), 0);
			else
				this.vScrollBar1.Value = Math.Max(Math.Min(e.NewValue, this.vScrollBar1.Maximum), 0);
			this.panel1.Location = new Point(-this.hScrollBar1.Value, -this.vScrollBar1.Value);
		}
	}
}
