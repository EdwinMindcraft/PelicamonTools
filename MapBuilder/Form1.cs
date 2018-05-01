using MapBuilder.Tiles;
using System.Windows.Forms;
using System.IO;
using MapBuilder.Utils;
using MapBuilder.Controls;

namespace MapBuilder {

	public partial class Form1 : Form {
        const int TILESETS_NUMBER = 2;


        public Form1() {
			InitializeComponent();
			/*
            for (int i = 0; i < TILESETS_NUMBER; i++)
            {
                this.tilesetPalette1.AvailableTilesets.Add(new Tileset(32));
                this.tilesetPalette1.AvailableTilesets[i].Index = i;
                this.tilesetPalette1.AvailableTilesets[i].TileUpdated += (sender, e) => { this.tilemapDesigner1.Tileset = this.tilesetPalette1.Tileset; };
            }
			//SANITY WARNING
			//The following code might hurt your soul/eyes/brain (delete as appropriate) due to its ugliness
			//Creators of this code is not responsible for any kind of physical/emotional/mental/other damage caused by it
			//Therefore it is recommended to continue with caution
			//WARNING END
			this.tilesetPalette1.AvailableTilesets[0].AddTileMap(Properties.Resources.Outside);
            this.tilesetPalette1.AvailableTilesets[0].AddTileMap(Properties.Resources.PlainColors, false);
            this.tilesetPalette1.AvailableTilesets[0].Name = "Outside";
            this.tilesetPalette1.AvailableTilesets[1].AddTileMap(Properties.Resources.PlainColors);
            this.tilesetPalette1.AvailableTilesets[1].Name = "Plain Colors";*/
            this.tilesetPalette1.FinishInitialisation();
            //You are now exiting the 'Crappy Code Zone'
            //If you're reading this message, congratulations for making it out alive.
			this.tilesetPalette1.OnTileSelect += (i, tileset) => { this.tilemapDesigner1.Selected = i; };
			this.tilesetPalette1.OnTilesetChange += (ts) => {
				this.tilemapDesigner1.Tileset = ts;
				//this.tilemapDesigner1.Redraw();
			};
			this.tilesetPalette1.Tileset.AddTileMap(Properties.Resources.Outside);
			this.tilemapDesigner1.Tilemap.Layers.Add(new TilemapLayer());
			this.tilemapDesigner1.OnTilePick += (i) => {
				this.tilesetPalette1.Selected = i;
				this.tilesetPalette1.Redraw();
			};
			this.tilemapDesigner1.Tilemap.UpdateTilemapSize(this.tilesetPalette1.Tileset, this.tilemapDesigner1.RenderSize);
			this.tilemapDesigner1.ActiveLayer = 0;
			this.tilemapDesigner1.UpdateLayerList();
		}
		
		private void binaryFileToolStripMenuItem_Click(object sender, System.EventArgs e) {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "Tile Map Binaries|*.tmb";
			if (dialog.ShowDialog() == DialogResult.OK) {
				File.WriteAllBytes(dialog.FileName, IOUtils.GenerateBinaries(this.tilemapDesigner1.Tilemap));
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
    }
}
