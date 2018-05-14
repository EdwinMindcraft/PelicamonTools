using Pelicamon.Common.Tiles;
using System.Collections.Generic;

namespace Pelicamon.Common.Utils {
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

		public static TilemapLayer LoadLayerFromBinaries(byte[] bytes, int start = 0) {
			TilemapLayer layer = new TilemapLayer();
			int w = layer.width = ByteUtils.GetInteger(start + 0, bytes);
			int h = layer.height = ByteUtils.GetInteger(start + 4, bytes);
			layer.UpdateLayerSize();
			for (int i = 0; i < h; i++) {
				for (int j = 0; j < w; j++) {
					int target = start + (i * layer.width + j) * 4 + 8;
					layer[j, i] = ByteUtils.GetInteger(target, bytes);
				}
			}
			return layer;
		}

		public static Tilemap LoadTilemapFromBinaries(byte[] bytes) {
			int s = ByteUtils.GetInteger(0, bytes);
			int w = ByteUtils.GetInteger(4, bytes);
			int h = ByteUtils.GetInteger(8, bytes);
			Tilemap map = new Tilemap(w, h);
			map.UpdateTilemapSize();
			var inc = 8 + w * h * 4;
			for (int i = 0; i < s; i++) {
				map.Layers.Add(LoadLayerFromBinaries(bytes, start: 12 + i * inc));
			}
			return map;
		}


	}
}
