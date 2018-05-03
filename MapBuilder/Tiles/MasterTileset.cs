using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapBuilder.Utils;
using System.Drawing;

namespace MapBuilder.Tiles {
	public class MasterTileset : Tileset {
		public List<Tileset> Childs;

		public MasterTileset(int size) : base (size) {
			this.Childs = new List<Tileset>();
		}

		public void AddChild(String name, params Image[] images) {
			Tileset child = new Tileset(this.TileSize);
			int s = 0;
			Childs.ForEach(ts => s += ts.Tiles.Count);
			child.StartIndex = s;
			child.Name = name;
			images.ToList().ForEach(img => {
				this.AddTileMap(img);
				child.AddTileMap(img);
			});
			Childs.Add(child);
		}

        public void AddChild(String name, int size, params Image[] images) {
			Tileset child = new Tileset(this.TileSize);
            int s = 0;
            Childs.ForEach(ts => s += ts.Tiles.Count);
            child.StartIndex = s;
            child.Name = name;
			images.ToList().ForEach(img => {
				Bitmap bitmap = new Bitmap((img.Width * this.TileSize) / size, (img.Height * this.TileSize) / size);
				using (Graphics g = Graphics.FromImage(bitmap)) {
					g.DrawImage(img, new Rectangle(Point.Empty, bitmap.Size), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
				}
				this.AddTileMap(bitmap);
				child.AddTileMap(bitmap);
			});
			Childs.Add(child);
        }

		public void AddAutoChild(String name, int size, AutotileImageFormat format, params Image[] images) {
			AutoTileset child = new AutoTileset(this.TileSize);
			int s = 0;
			Childs.ForEach(ts => s += ts.Tiles.Count);
			child.StartIndex = s;
			child.Name = name;
			images.ToList().ForEach(img => {
				Bitmap bitmap = new Bitmap((img.Width * this.TileSize) / size, (img.Height * this.TileSize) / size);
				using (Graphics g = Graphics.FromImage(bitmap)) {
					g.DrawImage(img, new Rectangle(Point.Empty, bitmap.Size), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
				}
				this.AddAutotile(format, bitmap);
				child.AddAutotile(format, bitmap);
			});
			Childs.Add(child);
		}

		public void AddAutotile(AutotileImageFormat imageFormat, Image source) {
			Bitmap[] bitmaps = ImageUtils.SplitForFormat(imageFormat, source, this.TileSize);
			int start = this.TilesData.Count;
			for (int i = 0; i < bitmaps.Length; i++) {
				Bitmap b = bitmaps[i];
				Bitmap[] subs = ImageUtils.GenerateAutotileBitmaps(imageFormat, b, this.TileSize);
				subs.ToList().ForEach(this.AddTileMap);
				for (int j = 0; j < 256; j++) {
					TileData td = this[start + i * 256 + j];
					td.Autotile = true;
					td.BaseID = start + i * 256;
					this[start + i * 256 + j] = td;
				}
			}
		}

		public void UpdateFromChildren() {
			this.TilesData.Clear();
			Childs.ForEach(l => l.TilesData.ForEach(k => this.TilesData.Add(k)));
		}

		public void UpdateChildren() {
			int cap = this.TilesData.Count;
			for (int i = Childs.Count - 1; i > 0; i--) {
				Childs[i].TilesData.Clear();
				for (int j = Childs[i].StartIndex; j < cap; j++) {
					TileData data = this.TilesData[j];
					Childs[i].TilesData.Add(data);
				}
				cap = Childs[i].StartIndex;
			}
		}
    }
}
