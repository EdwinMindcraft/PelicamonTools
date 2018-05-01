using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MapBuilder.Tiles {
	public class Tilemap {
		public int Width {
			get {
				return width;
			}
			set {
				this.prevW = this.width;
				this.width = value;
				this.UpdateTilemapSize();
			}
		}
		public int Height {
			get {
				return height;
			}
			set {
				this.prevH = this.height;
				this.height = value;
				this.UpdateTilemapSize();
			}
		}
		public TileData[] Tiles {
			get {
				return this.tiles;
			}
		}
		public Size Size { get {
				return new Size(width, height);
			}
		}

		private int prevW;
		private int prevH;
		private int width;
		private int height;
		private TileData[] tiles;
		public event EventHandler TilemapUpdated = (sender, e) => { };

		public Tilemap(int width, int height) {
			if (width < 0 || height < 0)
				throw new Exception("You know what ? FUCK YOU!");
			else if (width == 0 || height == 0)
				throw new ArgumentNullException(String.Format("Width and Heigth cannot be 0 ({0}x{1})", width, height));
			this.width = width;
			this.height = height;
			this.prevH = width;
			this.prevW = height;
			this.UpdateTilemapSize();
		}

		public void UpdateTilemapSize() {
			TileData[] newTiles = new TileData[width * height];
			if (tiles != null) {
				for (int i = 0; i < tiles.Length; i++) {
					int x = i % prevW;
					int y = (i - x) / prevH;
					int newID = x + y * width;
					if (newID > newTiles.Length)
						continue;
					newTiles[newID] = tiles[i];
				}
			}
			this.tiles = newTiles;
			if (TilemapUpdated != null)
				TilemapUpdated.Invoke(this, new EventArgs());
		}
	}
}
