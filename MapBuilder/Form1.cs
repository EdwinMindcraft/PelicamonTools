using MapBuilder.Tiles;
using System.Windows.Forms;
using System.IO;
using MapBuilder.Utils;
using MapBuilder.Controls;
using System;

namespace MapBuilder {

	public partial class Form1 : Form {


        public Form1() {
			InitializeComponent();
			//SANITY WARNING
			//The following code might hurt your soul/eyes/brain (delete as appropriate) due to its ugliness
			//Creators of this code is not responsible for any kind of physical/emotional/mental/other damage caused by it
			//Therefore it is recommended to continue with caution
			//WARNING END
			this.tilesetPalette1.FinishInitialisation();
			this.tilesetPalette1.OnTileSelect += (i, tileset) => { this.tilemapDesigner1.Selected = i; };
			this.tilesetPalette1.OnTilesetChange += (ts) => {
				this.tilemapDesigner1.Tileset = ts;
				this.tilemapDesigner1.Redraw();
			};
			this.tilemapDesigner1.Tilemap.Layers.Add(new TilemapLayer());
			this.tilemapDesigner1.OnTilePick += (i) => {
				this.tilesetPalette1.Selected = i;
				this.tilesetPalette1.Redraw();
			};
			this.tilemapDesigner1.Tilemap.UpdateTilemapSize(this.tilesetPalette1.Tileset, this.tilemapDesigner1.RenderSize);
			this.tilemapDesigner1.ActiveLayer = 0;
			this.tilemapDesigner1.UpdateLayerList();
			this.tilemapDesigner1.Redraw();
			this.tilesetPalette1.Redraw();
			//You are now exiting the 'Crappy Code Zone'
			//If you're reading this message, congratulations for making it out alive.
			//Lol Just kidding, this whole program is a mess.
		}

		private void binaryFileToolStripMenuItem_Click(object sender, System.EventArgs e) {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "Tile Map Binaries|*.tmb";
			if (dialog.ShowDialog() == DialogResult.OK) {
				File.WriteAllBytes(dialog.FileName, IOUtils.GenerateBinaries(this.tilemapDesigner1.Tilemap));
			}
		}

		private void binariesToolStripMenuItem_Click(object sender, System.EventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Tile Map Binaries|*.tmb";
			if (dialog.ShowDialog() == DialogResult.OK) {
				byte[] bytes = File.ReadAllBytes(dialog.FileName);
				try {
					Tilemap map = IOUtils.LoadTilemapFromBinaries(bytes);
					this.tilemapDesigner1.Tilemap = map;
					this.Redraw();
				} catch(Exception ex) {
					Console.WriteLine(ex);
				}
			}
		}

		public TilesetPalette GetTilesetPalette()
        {
            return this.tilesetPalette1;
        }

        public TilemapDesigner GetTilemapDesigner()
        {
            return this.tilemapDesigner1;
        }

		private void Redraw() {
			this.tilesetPalette1.Redraw();
			this.tilemapDesigner1.Redraw();
		}

		private void Form1_Resize(object sender, System.EventArgs e) {
			Redraw();
		}

		private void mapSizeToolStripMenuItem_Click(object sender, EventArgs e) {
			DialogMapSize dialog = new DialogMapSize(this.tilemapDesigner1.Tilemap.Width, this.tilemapDesigner1.Tilemap.Height);
			if (dialog.ShowDialog() == DialogResult.OK) {
				this.tilemapDesigner1.Tilemap.Size = new System.Drawing.Size(dialog.MapWidth, dialog.MapHeight);
			}
		}
	}
}
