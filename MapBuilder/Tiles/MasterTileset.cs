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
			//this.AddTileMap(image);
			//child.AddTileMap(image);
			Childs.Add(child);
		}

        public void AddChild(String name, int size, params Image[] images) {
            Tileset child = new Tileset(size);
            int s = 0;
            Childs.ForEach(ts => s += ts.Tiles.Count);
            child.StartIndex = s;
            child.Name = name;
			images.ToList().ForEach(img => {
				this.AddTileMap(img);
				child.AddTileMap(img);
			});
			//child.AddTileMap(image);
			//this.AddTileMap(image);
			Childs.Add(child);
        }
    }
}
