using System;
using MapBuilder.Tiles;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing.Imaging;

namespace MapBuilder.Controls {
	public partial class TilemapDesigner : UserControl {

		public int RenderSize { get; set; } = 32;
		public Tilemap Tilemap { get; set; } = new Tilemap(21, 15);
		public Tileset Tileset { get; set; }
		public int Selected { get; set; } = -1;
		public int ActiveLayer { get; set; } = -1;
		private Bitmap background;
		private Bitmap image;
		private bool dragging;

		public delegate void SelectEvent(int i);
		public event SelectEvent OnTilePick = new SelectEvent((i) => { });

		public TilemapDesigner() {
			InitializeComponent();
			GenerateBackground();
			GenerateImage();
			Tilemap.TilemapUpdated += this.Tilemap_TilemapUpdated;
		}

		private void Tilemap_TilemapUpdated(object sender, EventArgs e) {
			this.panel1.Size = new Size(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height);
			int w = this.panel1.Width - this.Width + 20;
			int h = this.panel1.Height - this.Height + 20;

			this.hScrollBar1.Value = 0;
			this.hScrollBar1.Minimum = 0;
			this.hScrollBar1.Maximum = w > 0 ? w : 0;
			this.hScrollBar1.Enabled = w > 0;

			this.vScrollBar1.Value = 0;
			this.vScrollBar1.Minimum = 0;
			this.vScrollBar1.Maximum = h > 0 ? h : 0;
			this.vScrollBar1.Enabled = h > 0;
			GenerateBackground();
			GenerateImage();
		}

		public void Redraw() {
			this.Tilemap.UpdateTilemapSize(Program.MasterTileset, RenderSize);
			this.GenerateBackground();
			this.GenerateImage();
			this.UpdateLayerList();
			this.panel1.Invalidate();
		}

		public void UpdateLayerList() {
			layerList.Items.Clear();
			layerList.SelectedIndices.Clear();
			for (int i = 0; i < Tilemap.Layers.Count; i++) {
				layerList.Items.Add(new ListViewItem(i != 0 ? "Layer " + i : "Background"));
			}
		}

		private void GenerateImage() {
			image = new Bitmap(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(image);
			g.DrawImage(background, Point.Empty);
			Tilemap.Layers.ForEach(l => g.DrawImage(l.LayerImage, 0, 0));
		}

		private void GenerateBackground() {
			background = new Bitmap(RenderSize * this.Tilemap.Width, RenderSize * this.Tilemap.Height, PixelFormat.Format32bppArgb);
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
			if (ActiveLayer < 0 || ActiveLayer >= Tilemap.Layers.Count)
				return;
			int x = (e.X - (e.X % RenderSize)) / RenderSize;
			int y = (e.Y - (e.Y % RenderSize)) / RenderSize;
			int target = Selected;
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
				if (e.Button == MouseButtons.Right)
					target = 0;
				if (Tilemap.Layers[ActiveLayer][x, y] == target)
					return;
				Tilemap.Layers[ActiveLayer][x, y] = target;
				Tilemap.Layers[ActiveLayer].GenerateImage(Program.MasterTileset, RenderSize);
				//Console.WriteLine("Drawing {0} at L{1}X{2}Y{3}", target, ActiveLayer, x, y);
				GenerateImage();
				panel1.Invalidate();
			} else if (e.Button == MouseButtons.Middle) {
				Selected = Tilemap.Layers[ActiveLayer][x, y];
				if (OnTilePick != null)
					OnTilePick.Invoke(Selected);
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(image, 0, 0);
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

		private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			dragging = true;
		}

		private void panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			dragging = false;
		}

		private void panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if (dragging) {
				Panel_MouseClick(sender, e);
			}
		}

		private void layerList_SelectedIndexChanged(object sender, EventArgs e) {
			if (layerList.SelectedIndices != null && layerList.SelectedIndices.Count > 0) {
				this.ActiveLayer = layerList.SelectedIndices[0];
				this.buttonRemoveLayer.Enabled = this.ActiveLayer != 0;
			} else {
				this.buttonRemoveLayer.Enabled = false;
			}
		}

		private void buttonAddLayer_Click(object sender, EventArgs e) {
			this.Tilemap.Layers.Add(new TilemapLayer());
			this.Redraw();
		}

		private void buttonRemoveLayer_Click(object sender, EventArgs e) {
			if (layerList.SelectedIndices != null && layerList.SelectedIndices.Count > 0) {
				this.buttonRemoveLayer.Enabled = false;
				this.Tilemap.Layers.RemoveAt(layerList.SelectedIndices[0]);
				this.Redraw();
			}
		}
	}
}
