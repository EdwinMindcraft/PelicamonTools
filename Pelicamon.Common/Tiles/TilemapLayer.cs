using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Pelicamon.Common.Tiles {
	public class TilemapLayer {

		public static MasterTileset Master { get; set; }

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
			if (LayerImage != null)
				LayerImage.Dispose();
			LayerImage = new Bitmap(width * size, height * size, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(LayerImage);
			g.Clear(Color.Transparent);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					int tile = tiles[x, y];
					if (tile < 0 || tile >= tileset.Tiles.Count)
						continue;
					if (tileset[tile].Autotile) {
						tile = tileset[tile].BaseID;
						tile += (int)GetAutoTileFormat(x, y);
						tiles[x, y] = tile;
					}
					g.DrawImage(tileset.Tiles[tile], new Rectangle(x * size, y * size, size, size));
				}
			}
			g.Dispose();
		}

		public void UpdateAutotiles() {
			List<TileData> tileset = Master.TilesData;
			int max = Master.Tiles.Count;
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					int tile = tiles[x, y];
					if (tile < 0 || tile >= max)
						continue;
					if (tileset[tile].Autotile) {
						tile = tileset[tile].BaseID;
						tile += (int)GetAutoTileFormat(x, y);
						tiles[x, y] = tile;
					}
				}
			}
		}

		public void UpdateAutotiles(int x, int y) {
			List<TileData> tileset = Master.TilesData;
			int max = Master.Tiles.Count;
			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					if (x + i < 0 || x + i >= width || y + j < 0 || y + j >= height)
						continue;
					int tile = tiles[x + i, y + j];
					if (tile < 0 || tile >= max)
						continue;
					if (tileset[tile].Autotile) {
						tile = tileset[tile].BaseID;
						tile += (int)GetAutoTileFormat(x, y);
						tiles[x + i, y + j] = tile;
					}
				}
			}
		}


		public AutoTileFormat GetAutoTileFormat(int x, int y) {
			List<TileData> tileset = Master.TilesData;
			int target = this.tiles[x, y];
			if (!tileset[target].Autotile)
				return AutoTileFormat.None;
			int n = y > 0 ? this.tiles[x, y - 1] : target;
			int s = y < height - 1 ? this.tiles[x, y + 1] : target;
			int w = x > 0 ? this.tiles[x - 1, y] : target;
			int e = x < width - 1 ? this.tiles[x + 1, y] : target;
			int nw = y > 0 && x > 0 ? this.tiles[x - 1, y - 1] : target;
			int ne = y > 0 && x < width - 1 ? this.tiles[x + 1, y - 1] : target;
			int sw = y < height - 1 && x > 0 ? this.tiles[x - 1, y + 1] : target;
			int se = y < height - 1 && x < width - 1 ? this.tiles[x + 1, y + 1] : target;
			target = tileset[target].BaseID;
			n = tileset[n].BaseID;
			s = tileset[s].BaseID;
			w = tileset[w].BaseID;
			e = tileset[e].BaseID;
			nw = tileset[nw].BaseID;
			ne = tileset[ne].BaseID;
			sw = tileset[sw].BaseID;
			se = tileset[se].BaseID;
			AutoTileFormat format = AutoTileFormat.None;
			if (n == target)
				format |= AutoTileFormat.ConnectNorth;
			if (s == target)
				format |= AutoTileFormat.ConnectSouth;
			if (w == target)
				format |= AutoTileFormat.ConnectWest;
			if (e == target)
				format |= AutoTileFormat.ConnectEast;
			if (nw == target)
				format |= AutoTileFormat.ConnectNorthWest;
			if (ne == target)
				format |= AutoTileFormat.ConnectNorthEast;
			if (sw == target)
				format |= AutoTileFormat.ConnectSouthWest;
			if (se == target)
				format |= AutoTileFormat.ConnectSouthEast;
			return format;
		}

		public AutoTileFormat[,] GetTileLinks() {
			AutoTileFormat[,] tiles = new AutoTileFormat[this.width, this.height];
			for (int i = 0; i < this.width; i++) {
				for (int j = 0; j < this.height; j++) {
					tiles[i, j] = GetAutoTileFormat(i, j);
				}
			}
			return tiles;
		}
	}
}
