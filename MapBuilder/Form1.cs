using MapBuilder.Tiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;



namespace MapBuilder {

	public partial class Form1 : Form {
        const int TILESETS_NUMBER = 2;

        public Form1() {
			InitializeComponent();
            for (int i = 0; i < TILESETS_NUMBER; i++)
            {
                this.tilesetPalette1.AvailableTilesets.Add(new Tileset(32));
                this.tilesetPalette1.AvailableTilesets[i].TileUpdated += (sender, e) => { this.tilemapDesigner1.Tileset = this.tilesetPalette1.Tileset; };
            }
            //SANITY WARNING
            //The following code might hurt your soul/eyes/brain (delete as appropriate) due to its ugliness
            //Creators of this code is not responsible for any kind of physical/emotional/mental/other damage caused by it
            //Therefore it is recommended to continue with caution
            //WARNING END
			this.tilesetPalette1.AvailableTilesets[0].AddTileMap(Properties.Resources.Outside);
            this.tilesetPalette1.AvailableTilesets[0].Name = "Outside";
            this.tilesetPalette1.AvailableTilesets[1].AddTileMap(Properties.Resources.PlainColors);
            this.tilesetPalette1.AvailableTilesets[1].Name = "Plain Colors";
            this.tilesetPalette1.FinishInitialisation();
            //You are now exiting the 'Crappy Code Zone'
            //If you're reading this message, congratulations for making it out alive.
			this.tilesetPalette1.OnTileSelect += (i, tileset) => { this.tilemapDesigner1.Selected = i; };
			this.tilesetPalette1.Tileset.AddTileMap(Properties.Resources.Outside);
			this.tilemapDesigner1.Tilemap.Layers.Add(new TilemapLayer());
			this.tilemapDesigner1.Tilemap.UpdateTilemapSize(this.tilesetPalette1.Tileset, this.tilemapDesigner1.RenderSize);
			this.tilemapDesigner1.ActiveLayer = 0;
			File.WriteAllBytes("./tileset.bin", this.tilesetPalette1.Tileset.ToByteArray());
		}
	}
}
