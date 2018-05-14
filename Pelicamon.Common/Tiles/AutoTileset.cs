using Pelicamon.Common.Utils;
using System.Linq;
using System.Drawing;

namespace Pelicamon.Common.Tiles {
	public class AutoTileset : Tileset {

		public AutoTileset(int size) : base (size) {
			UsesSpecial = true;
		}

		public void AddAutotile(AutotileImageFormat imageFormat, Image source) {
			Bitmap[] bitmaps = ImageUtils.SplitForFormat(imageFormat, source, this.TileSize);
			int start = this.TilesData.Count;
			for (int i = 0; i < bitmaps.Length; i++) {
				Bitmap b = bitmaps[i];
				Bitmap[] subs = ImageUtils.GenerateAutotileBitmaps(imageFormat, b, this.TileSize);
				subs.ToList().ForEach(this.AddTileMap);
				this.renderedTiles.Add(this.Tiles[start + i * 256]);
				this.renderedTileData.Add(this[start + i * 256]);
				for (int j = 0; j < 256; j++) {
					TileData td = this[start + i * 256 + j];
					td.Autotile = true;
					td.BaseID = this.StartIndex + start + i * 256;
					this[start + i * 256 + j] = td;
				}
			}
		}
	}
}
