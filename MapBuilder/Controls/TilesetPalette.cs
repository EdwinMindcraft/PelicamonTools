using MapBuilder.Tiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MapBuilder.Controls {
	public partial class TilesetPalette : UserControl {
		
        public Tileset Tileset {
            get {
				if (selectedTileset < 0 || selectedTileset >= Program.MasterTileset.Childs.Count)
					return new Tileset(32);
                return Program.MasterTileset.Childs[selectedTileset];
            }
        }

		private void UpdateScrollbar() {
			int i = Tileset.Tiles.Count;
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

        public delegate void TileSelectEvent(int id, Tileset sender);
		public delegate void TilsetSelectEvent(Tileset tileset);
        public event TileSelectEvent OnTileSelect = new TileSelectEvent((id, Tileset) => { });
		public event TilsetSelectEvent OnTilesetChange = new TilsetSelectEvent((ts) => { });

		public int RenderSize { get; set; } = 64;
		public int DisplayWidth { get { return (int)Math.Floor((float)panel1.Width / RenderSize); } }
		public int Selected { get; set; } = -1;
		private int selectedTileset = 0;

		public TilesetPalette() {
			InitializeComponent();
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
			if (Tileset != null) {
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

		public void Redraw() {
			int i = Tileset.Tiles.Count;
			int y = (i - (i % DisplayWidth)) / DisplayWidth;
			this.panel1.Size = new Size(panel1.Size.Width, y * RenderSize);
			this.UpdateScrollbar();
			panel1.Invalidate();
		}

		private void panel1_MouseClick(object sender, MouseEventArgs e) {
			int x = (int)Math.Floor((float)e.X / RenderSize);
			int y = (int)Math.Floor((float)e.Y / RenderSize);
			Selected = y * DisplayWidth + x;
			panel1.Invalidate();
            if (OnTileSelect != null)
            {
                int newSelect = Selected + this.Tileset.StartIndex;
                OnTileSelect.Invoke(newSelect, this.Tileset);
            }
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			this.selectedTileset = comboBox1.SelectedIndex;
			panel1.Invalidate();
			this.Selected = -1;
			if (OnTilesetChange != null)
				OnTilesetChange.Invoke(Tileset);
			this.Redraw();
		}
    }
}
