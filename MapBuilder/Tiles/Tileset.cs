using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json.Linq;
using MapBuilder.Utils;

namespace MapBuilder.Tiles {
	public class Tileset {

		public int TileSize { get; }
		public List<Image> Tiles { get; }
		public List<TileData> TilesData { get; }
		public List<Image> TileSetImages { get; }
		public event EventHandler TileUpdated = new EventHandler((sender, args)=> { });

		public Tileset(int tileSize) {
			this.TileSize = tileSize;
			this.Tiles = new List<Image>();
			this.TilesData = new List<TileData>();
			this.TileSetImages = new List<Image>();
		}

		public void AddTileMap(Image image) {
			Bitmap bitmap = new Bitmap(image);
			int currentImageID = TileSetImages.Count;
			TileSetImages.Add(bitmap);
			for (int i = 0; i < image.Height - TileSize; i += TileSize) {
				for (int j = 0; j < image.Width - TileSize; j += TileSize) {
					Rectangle target = new Rectangle(j, i, TileSize, TileSize);
					Bitmap tmp = bitmap.Clone(target, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					if (Tiles.Count == 0) {
						Graphics g = Graphics.FromImage(tmp);
						g.Clear(Color.Transparent);
						g.Dispose();
					}
					Tiles.Add(tmp);
					TilesData.Add(new TileData() { ImageID = currentImageID, X = i / TileSize, Y = j / TileSize, ID = Tiles.Count - 1});
				}
			}
			if (TileUpdated != null)
				TileUpdated.Invoke(this, new EventArgs());
		}

		public JArray ToJson() {
			JArray arr = new JArray();
			this.TilesData.ForEach((obj) => { arr.Add(obj.ToJson()); });
			return arr;
		}

		public byte[] ToByteArray() {
			byte[] bytes = new byte[this.TilesData.Count * 16 + 4];
			ByteUtils.SetInteger(0, ref bytes, this.TilesData.Count);
			for (int i = 0; i < this.TilesData.Count; i++) {
				int target = 4 + i * 16;
				TileData td = this.TilesData[i];
				ByteUtils.SetInteger(target + 0, ref bytes, td.ImageID);
				ByteUtils.SetInteger(target + 4, ref bytes, td.ID);
				ByteUtils.SetInteger(target + 8, ref bytes, td.X);
				ByteUtils.SetInteger(target + 12, ref bytes, td.Y);
			}
			return bytes;
		}

		public static List<TileData> FromByteArray(byte[] bytes) {
			int size = ByteUtils.GetInteger(0, bytes);
			List<TileData> list = new List<TileData>(size);
			for (int i = 0; i < size; i++) {
				int target = 4 + i * 16;
				list.Add(new TileData {
					ImageID = ByteUtils.GetInteger(target + 0, bytes),
					ID = ByteUtils.GetInteger(target + 4, bytes),
					X = ByteUtils.GetInteger(target + 8, bytes),
					Y = ByteUtils.GetInteger(target + 12, bytes)
				});
			}
			return list;
		}
	}

	public struct TileData {
		public int ImageID { get; set; }
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public static TileData FromJson(JObject token) {
			TileData tile = new TileData();
			tile.ID = (int)token["id"];
			tile.X = (int)token["x"];
			tile.Y = (int)token["y"];
			tile.ImageID = (int)token["image_id"];
			return tile;
		}

		public JObject ToJson() {
			return new JObject() {
				{ "id", this.ID },
				{ "x", this.X },
				{ "y", this.Y },
				{ "image_id", this.ImageID }
			};
		}
	}
}
