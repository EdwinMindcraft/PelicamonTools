using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapBuilder.Tiles {
	public class TilemapLayer {

		public int prevW;
		public int prevH;
		public int width;
		public int height;
		private TileData[] tiles;

		public void UpdateLayerSize() {
			TileData[] newTiles = new TileData[width * height];
			if (tiles != null) {
				for (int i = 0; i < tiles.Length; i++) {
					int x = i % prevW;
					int y = (i - x) / prevH;
					int newID = x + y * width;
					if (newID >= newTiles.Length)
						continue;
					newTiles[newID] = tiles[i];
				}
			}
			this.tiles = newTiles;
		}
	}
}
