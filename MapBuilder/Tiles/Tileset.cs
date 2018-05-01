using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MapBuilder.Tiles {
	public class Tileset {

		public int TileSize { get; }
		public List<Image> Tiles { get; }
		public List<TileData> TilesData { get; }
		public event EventHandler TileUpdated = new EventHandler((sender, args)=> { });

		private int imageId = 0;

		public Tileset(int tileSize) {
			this.TileSize = tileSize;
			this.Tiles = new List<Image>();
			this.TilesData = new List<TileData>();
		}

		public void AddTileMap(Image image) {
			Bitmap bitmap = new Bitmap(image);
			int currentImageID = this.imageId++;
			for (int i = 0; i < image.Height - TileSize; i += TileSize) {
				for (int j = 0; j < image.Width - TileSize; j += TileSize) {
					Rectangle target = new Rectangle(j, i, TileSize, TileSize);
					Tiles.Add(bitmap.Clone(target, System.Drawing.Imaging.PixelFormat.DontCare));
					TilesData.Add(new TileData() { ImageID = imageId, X = i / TileSize, Y = j / TileSize, ID = Tiles.Count - 1});
				}
			}
			if (TileUpdated != null)
				TileUpdated.Invoke(this, new EventArgs());
		}
	}

	public struct TileData {
		public int ImageID { get; set; }
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}
}
