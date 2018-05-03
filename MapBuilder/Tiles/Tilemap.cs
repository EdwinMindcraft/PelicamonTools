using System;
using System.Collections.Generic;
using System.Drawing;

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
		public List<TilemapLayer> Layers {
			get {
				return this.layers;
			}
		}
		public Size Size {
			get {
				return new Size(width, height);
			}
			set {
				this.prevW = this.width;
				this.prevH = this.height;
				this.width = value.Width;
				this.height = value.Height;
				this.UpdateTilemapSize();
			}
		}

		private int prevW;
		private int prevH;
		private int width;
		private int height;
		private List<TilemapLayer> layers;
		public event EventHandler TilemapUpdated = (sender, e) => { };

		public Tilemap(int width, int height) {
			if (width < 0 || height < 0)
				throw new Exception("You know what ? FUCK YOU!");
			else if (width == 0 || height == 0)
				throw new ArgumentNullException(String.Format("Width and Heigth cannot be 0 ({0}x{1})", width, height));
			this.width = width;
			this.height = height;
			this.prevW = width;
			this.prevH = height;
			this.layers = new List<TilemapLayer>();
		}

		public void UpdateTilemapSize(Tileset ts = null, int size = 0) {
			layers.ForEach((l) => {
				l.width = width;
				l.height = height;
				l.UpdateLayerSize();
				if (ts != null)
					l.GenerateImage(ts, size);
			});
			if (TilemapUpdated != null)
				TilemapUpdated.Invoke(this, new EventArgs());
		}
	}
	/// <summary>
	/// 1 Autotiles => 256 Regular tiles
	/// </summary>
	public enum AutoTileFormat {
		None = 0,
		ConnectNorth = 0x1,
		ConnectSouth = 0x2,
		ConnectEast = 0x4,
		ConnectWest = 0x8,
		ConnectNorthEast = 0x10,
		ConnectSouthEast = 0x20,
		ConnectNorthWest = 0x40,
		ConnectSouthWest = 0x80,
	}
}
