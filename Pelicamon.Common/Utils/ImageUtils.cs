using System.Collections.Generic;
using System.Drawing;
using Pelicamon.Common.Tiles;

namespace Pelicamon.Common.Utils {
	public static class ImageUtils {

		public static Bitmap SubImage(Bitmap input, Rectangle rect) {
			return input.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
		}

		public static Bitmap[] SplitForFormat(AutotileImageFormat format, Image source, int tilesize) {
			int w = format == AutotileImageFormat.RMMV ? 2 * tilesize : format == AutotileImageFormat.RMXP ? 3 * tilesize : tilesize;
			int h = format == AutotileImageFormat.RMMV ? 3 * tilesize : format == AutotileImageFormat.RMXP ? 4 * tilesize : tilesize;
			Bitmap bitmap = new Bitmap(source);
			List<Bitmap> childs = new List<Bitmap>();
			for (int i = 0; i <= bitmap.Width - w; i += w) {
				for (int j = 0; j <= bitmap.Height - h; j += h) {
					childs.Add(SubImage(bitmap, new Rectangle(i, j, w, h)));
				}
			}
			return childs.ToArray();
		}

		public static Bitmap[] GenerateAutotileBitmaps(AutotileImageFormat format, Image source, int tilesize) {
			Bitmap bitmap = new Bitmap(source);
			int half = tilesize / 2;
			Bitmap[] outputs = new Bitmap[256];
			Point locNW = Point.Empty;
			Point locNE = new Point(half, 0);
			Point locSW = new Point(0, half);
			Point locSE = new Point(half, half);
			if (format == AutotileImageFormat.RMMV) {
				//Corners
				Bitmap cornerNW = SubImage(bitmap, new Rectangle(0, tilesize, half, half));
				Bitmap cornerNE = SubImage(bitmap, new Rectangle(tilesize + half, tilesize, half, half));
				Bitmap cornerSW = SubImage(bitmap, new Rectangle(0, tilesize * 2 + half, half, half));
				Bitmap cornerSE = SubImage(bitmap, new Rectangle(tilesize + half, tilesize * 2 + half, half, half));
				//Inners
				Bitmap innerNW = SubImage(bitmap, new Rectangle(tilesize, 2 * tilesize, half, half));
				Bitmap innerNE = SubImage(bitmap, new Rectangle(half, 2 * tilesize, half, half));
				Bitmap innerSW = SubImage(bitmap, new Rectangle(tilesize, tilesize + half, half, half));
				Bitmap innerSE = SubImage(bitmap, new Rectangle(half, tilesize + half, half, half));
				//Outers
				Bitmap outerNW = SubImage(bitmap, new Rectangle(tilesize, 0, half, half));
				Bitmap outerNE = SubImage(bitmap, new Rectangle(tilesize + half, 0, half, half));
				Bitmap outerSW = SubImage(bitmap, new Rectangle(tilesize, half, half, half));
				Bitmap outerSE = SubImage(bitmap, new Rectangle(tilesize + half, half, half, half));
				//Verticals
				Bitmap vertNW = SubImage(bitmap, new Rectangle(0, tilesize * 2, half, half));
				Bitmap vertNE = SubImage(bitmap, new Rectangle(tilesize + half, tilesize * 2, half, half));
				Bitmap vertSW = SubImage(bitmap, new Rectangle(0, tilesize + half, half, half));
				Bitmap vertSE = SubImage(bitmap, new Rectangle(tilesize + half, tilesize + half, half, half));
				//Horizontals
				Bitmap horizNW = SubImage(bitmap, new Rectangle(tilesize, tilesize, half, half));
				Bitmap horizNE = SubImage(bitmap, new Rectangle(half, tilesize, half, half));
				Bitmap horizSW = SubImage(bitmap, new Rectangle(tilesize, tilesize * 2 + half, half, half));
				Bitmap horizSE = SubImage(bitmap, new Rectangle(half, tilesize * 2 + half, half, half));

				for (int i = 0; i < 256; i++) {
					AutoTileFormat f = (AutoTileFormat)i;
					bool connectN = (f & AutoTileFormat.ConnectNorth) == AutoTileFormat.ConnectNorth;
					bool connectS = (f & AutoTileFormat.ConnectSouth) == AutoTileFormat.ConnectSouth;
					bool connectW = (f & AutoTileFormat.ConnectWest) == AutoTileFormat.ConnectWest;
					bool connectE = (f & AutoTileFormat.ConnectEast) == AutoTileFormat.ConnectEast;
					bool connectNW = (f & AutoTileFormat.ConnectNorthWest) == AutoTileFormat.ConnectNorthWest;
					bool connectNE = (f & AutoTileFormat.ConnectNorthEast) == AutoTileFormat.ConnectNorthEast;
					bool connectSW = (f & AutoTileFormat.ConnectSouthWest) == AutoTileFormat.ConnectSouthWest;
					bool connectSE = (f & AutoTileFormat.ConnectSouthEast) == AutoTileFormat.ConnectSouthEast;
					Bitmap nw = connectN && connectW ? connectNW ? innerNW : outerNW : connectN ? vertNW : connectW ? horizNW : cornerNW;
					Bitmap ne = connectN && connectE ? connectNE ? innerNE : outerNE : connectN ? vertNE: connectE ? horizNE : cornerNE;
					Bitmap sw = connectS && connectW ? connectSW ? innerSW : outerSW : connectS ? vertSW : connectW ? horizSW : cornerSW;
					Bitmap se = connectS && connectE ? connectSE ? innerSE : outerSE : connectS ? vertSE : connectE ? horizSE : cornerSE;
					outputs[i] = new Bitmap(tilesize, tilesize);
					using (Graphics g = Graphics.FromImage(outputs[i])) {
						g.DrawImage(nw, locNW);
						g.DrawImage(ne, locNE);
						g.DrawImage(sw, locSW);
						g.DrawImage(se, locSE);
					}
				}
				#region Memory cleanup
				cornerNW.Dispose();
				cornerNE.Dispose();
				cornerSW.Dispose();
				cornerSE.Dispose();
				//Inners
				innerNW.Dispose();
				innerNE.Dispose();
				innerSW.Dispose();
				innerSE.Dispose();
				//Outers
				outerNW.Dispose();
				outerNE.Dispose();
				outerSW.Dispose();
				outerSE.Dispose();
				//Verticals
				vertNW.Dispose();
				vertNE.Dispose();
				vertSW.Dispose();
				vertSE.Dispose();
				//Horizontals
				horizNW.Dispose();
				horizNE.Dispose();
				horizSW.Dispose();
				horizSE.Dispose();
				#endregion
			} else if (format == AutotileImageFormat.RMXP) {

			}
			return outputs;
		}
	}

	public enum AutotileImageFormat {
		RMXP,
		RMMV
	}
}
