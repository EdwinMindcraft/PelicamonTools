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

       // public List<Tileset> AvailableTilesets = new List<Tileset>();

        private void Tileset_TileUpdated(object sender, EventArgs e) {
            this.vScrollBar1.Minimum = 0;
            int i = Tileset.Tiles.Count - 1;
            int y = (i - (i % DisplayWidth)) / DisplayWidth;
            this.vScrollBar1.Maximum = y + 1;
            this.panel1.VerticalScroll.Maximum = (y + 1) * RenderSize;
            this.panel1.VerticalScroll.Minimum = 0;
            this.panel1.VerticalScroll.Enabled = true;
            this.panel1.VerticalScroll.Visible = false;
            this.panel1.Size = new Size(panel1.Size.Width, y * RenderSize);
            panel1.Invalidate();
        }

        public delegate void TileSelectEvent(int id, Tileset sender);
		public delegate void TilsetSelectEvent(Tileset tileset);
        public event TileSelectEvent OnTileSelect = new TileSelectEvent((id, Tileset) => { });
		public event TilsetSelectEvent OnTilesetChange = new TilsetSelectEvent((ts) => { });

		public int RenderSize { get; set; } = 64;
		public int DisplayWidth { get { return (int)Math.Floor((float)panel1.Width / RenderSize); } }
		private int yOffset = 0;
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
				set.TileUpdated += this.Tileset_TileUpdated;
			}
            this.comboBox1.SelectedIndex = 0;
        }

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
			this.vScrollBar1.Value = e.NewValue;
			this.yOffset = e.NewValue;
			panel1.Location = new Point(0, -this.yOffset * RenderSize);
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
			if (Tileset != null) {
				for (int i = 0; i < Tileset.VisibleTiles.Count; i++) {
					int x = i % DisplayWidth;
					int y = (i - x) / DisplayWidth;
					x *= RenderSize;
					y *= RenderSize;
					Image tile = Tileset.VisibleTiles[i];
					e.Graphics.DrawImage(tile, new Rectangle(x, y, RenderSize, RenderSize));
				}
				if (Selected != -1) {
					e.Graphics.DrawRectangle(Pens.White, new Rectangle((Selected % DisplayWidth) * RenderSize, ((Selected - (Selected % DisplayWidth)) / DisplayWidth) * RenderSize, RenderSize - 1, RenderSize - 1));
				}
			}
		}

		public void Redraw() {
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
				/*
                if (Tileset.Index != 0)
                {
                    for (int j = 0; j < Tileset.Index; j++)
                    {
                        newSelect += AvailableTilesets[j].VisibleTiles.Count;
                    }
                    //Console.Out.WriteLine("MAX SIZE : " + newSelect.ToString());
                    newSelect += Selected;
                }
                else
                {
                    newSelect = Selected;
                }*/
                OnTileSelect.Invoke(newSelect, this.Tileset);
            }
		}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			this.selectedTileset = comboBox1.SelectedIndex;
			panel1.Invalidate();
			if (OnTilesetChange != null)
				OnTilesetChange.Invoke(Tileset);
        }
    }
}
