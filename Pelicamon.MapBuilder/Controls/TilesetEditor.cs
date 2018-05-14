using Pelicamon.Common.Tiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
	public partial class TilesetEditor : Form {

		public Tileset Tileset {
			get {
				return Program.MasterTileset.Childs[this.comboBox1.SelectedIndex];
			}
		}

		public int RenderSize { get; set; } = 32;
		public int DisplayWidth {
			get {
				return (doubleBufferedPanel1.Width - (doubleBufferedPanel1.Width % RenderSize)) / RenderSize;
			}
		}

		private TilesetEditionMode mode = TilesetEditionMode.Passage;
		private Image tilesetImage, background;
		private Image circle, cross;


		private void UpdateScrollbar() {
			int i = Tileset.Tiles.Count;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			int max = y - (panel1.Height - (panel1.Height % RenderSize)) / RenderSize;
			this.doubleBufferedPanel1.Height = y * RenderSize;
			this.vScrollBar1.Enabled = max > 0;
			this.vScrollBar1.Minimum = 0;
			this.vScrollBar1.Maximum = max > 0 ? max + this.vScrollBar1.LargeChange : 0;
			this.vScrollBar1.Refresh();
			this.vScrollBar1.Maximum = max > 0 ? max + this.vScrollBar1.LargeChange : 0;
			this.vScrollBar1.Value = this.vScrollBar1.Value > this.vScrollBar1.Maximum ? 0 : this.vScrollBar1.Value;
			this.doubleBufferedPanel1.Location = new Point(0, -this.vScrollBar1.Value * RenderSize);
			this.circle = Properties.Resources.Circle;
			this.cross = Properties.Resources.Cross;
		}

		private void GenerateTilesetImage() {
			if (tilesetImage != null)
				tilesetImage.Dispose();
			int i = Tileset.Tiles.Count;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			tilesetImage = new Bitmap(RenderSize * DisplayWidth, RenderSize * y);
			using (Graphics g = Graphics.FromImage(tilesetImage)) {
				for (int k = 0; k < Tileset.Tiles.Count; k++) {
					int x = k % DisplayWidth;
					int z = (k - x) / DisplayWidth;
					Rectangle target = new Rectangle(x * RenderSize, z * RenderSize, RenderSize, RenderSize);
					g.DrawImage(Tileset.Tiles[k], target);
				}
			}
		}

		private void GenerateBackground() {
			int h = (Tileset.Tiles.Count - (Tileset.Tiles.Count % DisplayWidth)) / DisplayWidth;
			background = new Bitmap(RenderSize * DisplayWidth, RenderSize * h);
			using (Graphics g = Graphics.FromImage(background)) {
				for (int i = 0; i < doubleBufferedPanel1.Width; i += RenderSize / 2) {
					for (int j = 0; j < doubleBufferedPanel1.Height; j += RenderSize / 2) {
						int k = (i * 2) / RenderSize + (j * 2) / RenderSize;
						g.FillRectangle(k % 2 == 1 ? Brushes.LightGray : Brushes.DarkGray, new Rectangle(i, j, RenderSize / 2, RenderSize / 2));
					}
				}
			}
		}

		public TilesetEditor() {
			InitializeComponent();
			this.comboBox1.Items.AddRange(Program.MasterTileset.Childs.ConvertAll((t) => t.Name).ToArray());
			this.comboBox1.SelectedIndex = 0;
			UpdateScrollbar();
			GenerateBackground();
			GenerateTilesetImage();
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(background, Point.Empty);
			e.Graphics.DrawImage(tilesetImage, Point.Empty);
			if (Tileset != null) {
				for (int i = 0; i < Tileset.Tiles.Count; i++) {
					int x = i % DisplayWidth;
					int y = (i - x) / DisplayWidth;
					Rectangle target = new Rectangle(x * RenderSize, y * RenderSize, RenderSize, RenderSize);
					switch (mode) {
						case TilesetEditionMode.Passage:
							RenderPassage(e.Graphics, i, target);
							break;
						default:
							break;
					}
				}
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			this.UpdateScrollbar();
			this.GenerateBackground();
			this.GenerateTilesetImage();
			this.doubleBufferedPanel1.Invalidate();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			this.vScrollBar1.Value = e.NewValue;
			doubleBufferedPanel1.Location = new Point(0, -this.vScrollBar1.Value * RenderSize);
		}

		private void panel1_MouseWheel(object sender, MouseEventArgs eventArgs) {
			int newVal = this.vScrollBar1.Value - eventArgs.Delta / 120;
			if (newVal < 0)
				newVal = 0;
			if (newVal > this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange + 1)
				newVal = this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange + 1;
			vScrollBar1_Scroll(doubleBufferedPanel1, new ScrollEventArgs(ScrollEventType.LargeDecrement, newVal));
		}

		private void doubleBufferedPanel1_MouseClick(object sender, MouseEventArgs e) {
			int x = (int)Math.Floor((float)e.X / RenderSize);
			int y = (int)Math.Floor((float)e.Y / RenderSize);
			int id = y * DisplayWidth + x;
			switch (mode) {
				case TilesetEditionMode.Passage:
					ClickPassge(id);
					break;
				default:
					break;
			}
		}

		//Action Functions
		private void RenderPassage(Graphics g, int tileId, Rectangle target) {
			TileData data = Tileset[tileId];
			g.DrawImage(data.Passage ? circle : cross, target);
		}

		private void ClickPassge(int id) {
			TileData td = Tileset[id];
			td.Passage = !td.Passage;
			Tileset[id] = td;
			this.doubleBufferedPanel1.Invalidate();
		}
	}

	public enum TilesetEditionMode {
		Passage,
		Animation
	}
}
