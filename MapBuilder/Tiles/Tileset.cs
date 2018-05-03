using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json.Linq;
using MapBuilder.Utils;
using System.Drawing.Drawing2D;

namespace MapBuilder.Tiles {
	public class Tileset {
		public const int LATEST_VERSION = 2;

		public int TileSize { get; set; }
        public List<Image> Tiles { get; set; }
		public List<Image> RenderedTiles {
			get {
				return UsesSpecial ? renderedTiles : Tiles;
			}
		}
		public List<TileData> TilesData { get; }
		public List<TileData> RenderedTileData {
			get {
				return UsesSpecial ? renderedTileData : TilesData;
			}
		}
		public int StartIndex { get; set; }
        public int Index;
		//public List<Image> TileSetImages { get; }
		public event EventHandler TileUpdated = new EventHandler((sender, args)=> { });
        public string Name { get; set; }
		protected bool UsesSpecial { get; set; } = false;
		public TileData this[int i] {
			get {
				return TilesData[i];
			}
			set {
				TilesData[i] = value;
			}
		}

		protected List<Image> renderedTiles;
		protected List<TileData> renderedTileData;

		public Tileset(int tileSize) {
			this.TileSize = tileSize;
			this.Tiles = new List<Image>();
			this.TilesData = new List<TileData>();
			//this.TileSetImages = new List<Image>();
			this.renderedTiles = new List<Image>();
			this.renderedTileData = new List<TileData>();
			this.StartIndex = 0;
		}

		public void AddTileMap(Image image) {
            Bitmap bitmap = new Bitmap(image);
			//int currentImageID = TileSetImages.Count + StartIndex;
			//TileSetImages.Add(bitmap);
			for (int i = 0; i <= image.Height - TileSize; i += TileSize) {
				for (int j = 0; j <= image.Width - TileSize; j += TileSize) {
                    Rectangle target = new Rectangle(j, i, TileSize, TileSize);
                    Bitmap tmp = bitmap.Clone(target, System.Drawing.Imaging.PixelFormat.DontCare);
					Tiles.Add(tmp);
					TilesData.Add(new TileData(this.StartIndex + Tiles.Count - 1, i / TileSize, j / TileSize));
				}
			}
			bitmap.Dispose();
			if (TileUpdated != null)
				TileUpdated.Invoke(this, new EventArgs());
		}

		public JArray ToJson() {
			JArray arr = new JArray();
			this.TilesData.ForEach((obj) => { arr.Add(obj.ToJson()); });
			return arr;
		}

		public byte[] ToByteArray() {
			byte[] bytes = new byte[this.TilesData.Count * TileData.GetSizeForVersion(LATEST_VERSION) + 8];
			ByteUtils.SetInteger(0, ref bytes, LATEST_VERSION);
			ByteUtils.SetInteger(4, ref bytes, this.TilesData.Count);
			for (int i = 0; i < this.TilesData.Count; i++) {
				int target = 8 + i * TileData.GetSizeForVersion(LATEST_VERSION);
				TileData td = this.TilesData[i];
				td.ToByteArray(LATEST_VERSION, ref bytes, target);
			}
			return bytes;
		}

		public static List<TileData> FromByteArray(byte[] bytes) {
			int version = ByteUtils.GetInteger(0, bytes);
			int size = ByteUtils.GetInteger(4, bytes);
			List<TileData> list = new List<TileData>(size);
			for (int i = 0; i < size; i++) {
				int target = 8 + i * TileData.GetSizeForVersion(version);
				list.Add(TileData.FromByteArray(version, bytes, target));
			}
			return list;
		}
	}

	public struct TileData {
		public int ID { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public bool Passage { get; set; }
		public bool Animated { get; set; }
		public bool Autotile { get; set; }
		public int BaseID { get; set; }

		public TileData(int ID, int X, int Y) {
			this.ID = ID;
			this.X = X;
			this.Y = Y;
			this.Passage = true;
			this.Animated = false;
			this.Autotile = false;
			this.BaseID = -1;
		}

		public void LoadUnsavedDataFrom(TileData other) {
			this.BaseID = other.BaseID;
			this.Autotile = other.Autotile;
		}

		public static TileData FromJson(JObject token) {
			TileData tile = new TileData();
			tile.ID = (int)token["id"];
			tile.X = (int)token["x"];
			tile.Y = (int)token["y"];
			tile.Animated = (bool)token["animated"];
			tile.Passage = (bool)token["passage"];
			return tile;
		}

		public JObject ToJson() {
			return new JObject() {
				{ "id", this.ID },
				{ "x", this.X },
				{ "y", this.Y },
				{ "passage", this.Passage },
				{ "animated", this.Animated }
			};
		}

		public static int GetSizeForVersion(int version) {
			switch (version) {
				case 1: return 17;
				case 2: return 13;
				default: return 0;
			}
		}

		public void ToByteArray(int version, ref byte[] bytes, int position = 0) {
			if (version == 1) {
				ByteUtils.SetInteger(position + 0, ref bytes, 0);
				ByteUtils.SetInteger(position + 4, ref bytes, this.ID);
				ByteUtils.SetInteger(position + 8, ref bytes, this.X);
				ByteUtils.SetInteger(position + 12, ref bytes, this.Y);
				byte flags = 0;
				if (Passage)
					flags |= 0x1;
				if (Animated)
					flags |= 0x2;
				bytes[position + 16] = flags;
			} else if (version == 2) {
				ByteUtils.SetInteger(position + 0, ref bytes, this.ID);
				ByteUtils.SetInteger(position + 4, ref bytes, this.X);
				ByteUtils.SetInteger(position + 8, ref bytes, this.Y);
				byte flags = 0;
				if (Passage)
					flags |= 0x1;
				if (Animated)
					flags |= 0x2;
				bytes[position + 12] = flags;
			}
		}

		public static TileData FromByteArray(int version, byte[] bytes, int position = 0) {
			TileData td = new TileData();
			if (version == 1) {
				td.ID = ByteUtils.GetInteger(position + 4, bytes);
				td.X = ByteUtils.GetInteger(position + 8, bytes);
				td.Y = ByteUtils.GetInteger(position + 12, bytes);
				byte flags = bytes[position + 16];
				td.Passage = (flags & 0x1) == 0x1;
				td.Animated = (flags & 0x2) == 0x2;
			} else if (version == 2) {
				td.ID = ByteUtils.GetInteger(position + 0, bytes);
				td.X = ByteUtils.GetInteger(position + 4, bytes);
				td.Y = ByteUtils.GetInteger(position + 8, bytes);
				byte flags = bytes[position + 12];
				td.Passage = (flags & 0x1) == 0x1;
				td.Animated = (flags & 0x2) == 0x2;
			}
			return td;
		}
	}
}
