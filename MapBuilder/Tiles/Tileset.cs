using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MapBuilder.Tiles {
	public class Tileset {

		public int TileSize { get; }
		public List<Image> Tiles { get; }
		public event EventHandler TileUpdated = new EventHandler((sender, args)=> { });

		public Tileset(int tileSize) {
			this.TileSize = tileSize;
			this.Tiles = new List<Image>();
		}

		public void AddTileMap(Image image) {
			Bitmap bitmap = new Bitmap(image); 
			for (int i = 0; i < image.Height - TileSize; i += TileSize) {
				for (int j = 0; j < image.Width - TileSize; j += TileSize) {
					Rectangle target = new Rectangle(j, i, TileSize, TileSize);
					Tiles.Add(bitmap.Clone(target, System.Drawing.Imaging.PixelFormat.DontCare));
				}
			}
			if (TileUpdated != null)
				TileUpdated.Invoke(this, new EventArgs());
		}
	}
}
