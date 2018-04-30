using MapBuilder.Tiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapBuilder {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			this.tilesetPalette1.Tileset = new Tileset(32);
			this.tilesetPalette1.Tileset.AddTileMap(Properties.Resources.Outside);
		}
	}
}
