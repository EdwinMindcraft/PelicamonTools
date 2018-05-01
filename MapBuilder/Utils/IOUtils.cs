using MapBuilder.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapBuilder.Utils {
	public static class IOUtils {
		/// <summary>
		/// Returns a binary encoding of the tilemap. Size : 8 + W * H * 4
		/// </summary>
		/// <param name="layer"></param>
		/// <returns></returns>
		public static byte[] GenerateBinaries(TilemapLayer layer) {
			byte[] bytes = new byte[layer.height * layer.width * 4 + 8];
			ByteUtils.SetInteger(0, ref bytes, layer.width);
			ByteUtils.SetInteger(4, ref bytes, layer.height);
			for (int i = 0; i < layer.height; i++) {
				for (int j = 0; j < layer.width; j++) {
					int target = (i * layer.width + j) * 4 + 8;
					ByteUtils.SetInteger(target, ref bytes, layer[j, i]);
				}
			}
			return bytes;
		}

		public static byte[] GenerateBinaries(Tilemap map) {
			List<byte> layers = new List<byte>();
			map.Layers.ForEach(l => layers.AddRange(GenerateBinaries(l)));
			byte[] bytes = new byte[12 + layers.Count];
			ByteUtils.SetInteger(0, ref bytes, map.Layers.Count);
			ByteUtils.SetInteger(4, ref bytes, map.Width);
			ByteUtils.SetInteger(8, ref bytes, map.Height);
			for (int i = 0; i < layers.Count; i++) {
				bytes[12 + i] = layers[i];
			}
			return bytes;
		}
	}
}
