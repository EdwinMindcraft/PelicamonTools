using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MapBuilder.Tiles {
	public class TilemapLayer {

		public Bitmap LayerImage { get; internal set; }

		public int width;
		public int height;
		private int[,] tiles;

		public int this[int x, int y] {
			get {
				if (x < 0 || x >= this.tiles.GetLength(0) || y < 0 || y >= this.tiles.GetLength(1))
					return -1;
				return this.tiles[x, y];
			}
			set {
				if (x < 0 || x >= this.tiles.GetLength(0) || y < 0 || y >= this.tiles.GetLength(1))
					return;
				this.tiles[x, y] = value;
			}
		}

		public void UpdateLayerSize() {
			int[,] newTiles = new int[width, height];
			if (tiles != null) {
				for (int x = 0; x < tiles.GetLength(0); x++) {
					if (x >= width)
						break;
					for (int y = 0; y < tiles.GetLength(1); y++) {
						if (y >= height)
							break;
						newTiles[x, y] = tiles[x, y];
					}
				}
			}
			this.tiles = newTiles;
		}

		public void GenerateImage(Tileset tileset, int size) {
			LayerImage = new Bitmap(width * size, height * size, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(LayerImage);
			g.Clear(Color.Transparent);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					int tile = tiles[x, y];
					if (tile < 0 || tile >= tileset.Tiles.Count)
						continue;
					g.DrawImage(tileset.Tiles[tile], new Rectangle(x * size, y * size, size, size));
				}
			}
			g.Dispose();
			Console.WriteLine(LayerImage.GetPixel(0, 0));
		}
	}
}
