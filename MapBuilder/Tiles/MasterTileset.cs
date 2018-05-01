using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapBuilder.Tiles {
	public class MasterTileset : Tileset {
		public List<Tileset> Childs;

		public MasterTileset(int size) : base (size) {
			this.Childs = new List<Tileset>();
		}

		public void AddChild(Image image, String name) {
			this.AddTileMap(image);
			Tileset child = new Tileset(this.TileSize);
			int s = 0;
			Childs.ForEach(ts => s += ts.Tiles.Count);
			child.StartIndex = s;
			child.Name = name;
			child.AddTileMap(image);
			Childs.Add(child);
		}
	}
}
