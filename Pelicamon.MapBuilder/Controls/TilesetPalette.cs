using Pelicamon.Common.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
	public partial class TilesetPalette : UserControl {
		
        public Tileset Tileset {
            get {
				if (selectedTileset < 0 || selectedTileset >= Program.MasterTileset.Childs.Count)
					return new Tileset(32);
                return Program.MasterTileset.Childs[selectedTileset];
            }
        }

		private void UpdateScrollbar() {
			int i = Tileset.RenderedTiles.Count;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			int max = y - (panel2.Height - (panel2.Height % RenderSize)) / RenderSize;
			this.vScrollBar1.Enabled = max > 0;
			this.vScrollBar1.Minimum = 0;
			this.vScrollBar1.Maximum = max > 0 ? max + this.vScrollBar1.LargeChange : 0;
			this.vScrollBar1.Refresh();
			this.vScrollBar1.Maximum = max > 0 ? max + this.vScrollBar1.LargeChange : 0;
			this.vScrollBar1.Value = this.vScrollBar1.Value > this.vScrollBar1.Maximum ? 0 : this.vScrollBar1.Value;
			this.panel1.Location = new Point(0, -this.vScrollBar1.Value * RenderSize);
		}

        public delegate void TileSelectEvent(int[,] selected, Tileset sender);
		public delegate void TilsetSelectEvent(Tileset tileset);
        public event TileSelectEvent OnTileSelect = new TileSelectEvent((id, Tileset) => { });
		public event TilsetSelectEvent OnTilesetChange = new TilsetSelectEvent((ts) => { });

		public int RenderSize { get; set; } = 64;
		public int DisplayWidth { get { return (int)Math.Floor((float)panel1.Width / RenderSize); } }
		public int[,] Selected { get; set; } = new int[0,0]; //PosX, PosY => ID
		private int selectedTileset = 0;
		private int[] selectedDraw = new int[4];
		private bool dragging = false;
		private int sx, sy;
		private Image background;

		public TilesetPalette() {
			InitializeComponent();
		}

		private void GenerateBackground() {
			if (background != null)
				background.Dispose();
			int h = (Tileset.Tiles.Count - (Tileset.Tiles.Count % DisplayWidth)) / DisplayWidth;
			background = new Bitmap(RenderSize * DisplayWidth, RenderSize * h);
			using (Graphics g = Graphics.FromImage(background)) {
				for (int i = 0; i < panel1.Width; i += RenderSize / 2) {
					for (int j = 0; j < panel1.Height; j += RenderSize / 2) {
						int k = (i * 2) / RenderSize + (j * 2) / RenderSize;
						g.FillRectangle(k % 2 == 1 ? Brushes.LightGray : Brushes.DarkGray, new Rectangle(i, j, RenderSize / 2, RenderSize / 2));
					}
				}
			}
		}

		public void FinishInitialisation()
        {
            foreach (Tileset set in Program.MasterTileset.Childs)
            {
                this.comboBox1.Items.Add(set.Name);
			}
            this.comboBox1.SelectedIndex = 0;
        }

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			this.vScrollBar1.Value = e.NewValue;
			panel1.Location = new Point(0, -this.vScrollBar1.Value * RenderSize);
		}

		private void panel1_MouseWheel(object sender, MouseEventArgs eventArgs) {
			int newVal = this.vScrollBar1.Value - eventArgs.Delta / 120;
			if (newVal < 0)
				newVal = 0;
			if (newVal > this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange + 1)
				newVal = this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange + 1;
			vScrollBar1_Scroll(panel1, new ScrollEventArgs(ScrollEventType.LargeDecrement, newVal));
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(background, Point.Empty);
			if (Tileset != null) {
				for (int i = 0; i < Tileset.RenderedTiles.Count; i++) {
					int x = i % DisplayWidth;
					int y = (i - x) / DisplayWidth;
					x *= RenderSize;
					y *= RenderSize;
					Image tile = Tileset.RenderedTiles[i];
					e.Graphics.DrawImage(tile, new Rectangle(x, y, RenderSize, RenderSize));
				}
				if (Selected.Length != 0) {
					Rectangle rect = new Rectangle(selectedDraw[0], selectedDraw[1], selectedDraw[2], selectedDraw[3]);
					if (rect.Height > 0 && rect.Width > 0)
						e.Graphics.DrawRectangle(Pens.White, rect);
				}
			}
		}

		public void Redraw() {
			int i = Tileset.RenderedTiles.Count;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			this.panel1.Size = new Size(panel1.Size.Width, y * RenderSize);
			this.UpdateScrollbar();
			this.UpdateSelectionBox();
			GenerateBackground();
			panel1.Invalidate();
		}

		private void UpdateSelectionBox() {
			if (Selected.Length > 0) {
				int x = Selected[0, 0] % DisplayWidth;
				int y = (Selected[0, 0] - x) / DisplayWidth;
				this.selectedDraw[0] = x * RenderSize;
				this.selectedDraw[1] = y * RenderSize;
				this.selectedDraw[2] = this.Selected.GetLength(0) * RenderSize;
				this.selectedDraw[3] = this.Selected.GetLength(1) * RenderSize;
			}
		}

		private void PostTileSelectEvent() {
			if (OnTileSelect != null) {
				/*
				int[,] newSelect = new int[this.Selected.GetLength(0), this.Selected.GetLength(1)];
				for (int i = 0; i < Selected.GetLength(0); i++) {
					for (int j = 0; j < Selected.GetLength(1); j++) {
						newSelect[i, j] = this.Selected[i, j];
					}
				}*/
				OnTileSelect.Invoke(Selected, this.Tileset);
			}
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			this.selectedTileset = comboBox1.SelectedIndex;
			panel1.Invalidate();
			this.Selected = new int[0, 0];
			if (OnTilesetChange != null)
				OnTilesetChange.Invoke(Tileset);
			this.Redraw();
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e) {
			dragging = true;
			sx = e.X;
			sy = e.Y;
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e) {
			if (dragging) {
				SelectArea(e.X, e.Y);
				UpdateSelectionBox();
				panel1.Invalidate();
				PostTileSelectEvent();
			}
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e) {
			dragging = false;
			SelectArea(e.X, e.Y);
			UpdateSelectionBox();
			panel1.Invalidate();
			PostTileSelectEvent();
		}

		private void SelectArea(int x, int y) {
			int sx = (int)Math.Floor((float)Math.Min(x, this.sx) / RenderSize);
			int sy = (int)Math.Floor((float)Math.Min(y, this.sy) / RenderSize);
			int ex = (int)Math.Ceiling((float)Math.Max(x, this.sx) / RenderSize);
			int ey = (int)Math.Ceiling((float)Math.Max(y, this.sy) / RenderSize);
			Selected = new int[ex - sx, ey - sy];
			for (int i = 0; i < Selected.GetLength(0); i++) {
				for (int j = 0; j < Selected.GetLength(1); j++) {
					int id = (sy + j) * DisplayWidth + sx + i;
					if (id < 0 || id >= Tileset.RenderedTileData.Count)
						continue;
					Selected[i, j] = Tileset.RenderedTileData[id].ID;
				}
			}
		}
	}
}
