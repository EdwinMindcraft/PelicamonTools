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
		public Form1() {
			InitializeComponent();
			this.tilesetPalette1.Tileset = new Tileset(32);
			this.tilesetPalette1.Tileset.TileUpdated += (sender, e) => { this.tilemapDesigner1.Tileset = this.tilesetPalette1.Tileset; };
			this.tilesetPalette1.OnTileSelect += (i) => { this.tilemapDesigner1.Selected = i; };
			this.tilesetPalette1.Tileset.AddTileMap(Properties.Resources.Outside);
			this.tilemapDesigner1.Tilemap.Layers.Add(new TilemapLayer());
			this.tilemapDesigner1.Tilemap.UpdateTilemapSize(this.tilesetPalette1.Tileset, this.tilemapDesigner1.RenderSize);
			this.tilemapDesigner1.ActiveLayer = 0;
			File.WriteAllBytes("./tileset.bin", this.tilesetPalette1.Tileset.ToByteArray());
		}
	}
}
