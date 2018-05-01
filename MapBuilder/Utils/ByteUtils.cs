namespace MapBuilder.Utils {
	public static class ByteUtils {
		public static void SetInteger(int position, ref byte[] bytes, int value) {
			bytes[position + 0] = (byte) ((value & 0xFF000000) >> 24);
			bytes[position + 1] = (byte) ((value & 0x00FF0000) >> 16);
			bytes[position + 2] = (byte) ((value & 0x0000FF00) >> 8);
			bytes[position + 3] = (byte) ((value & 0x000000FF) >> 0);
		}

		public static int GetInteger(int position, byte[] bytes) {
			int x24 = bytes[position + 0];
			int x16 = bytes[position + 1];
			int x8 = bytes[position + 2];
			int x0 = bytes[position + 3];
			x24 <<= 24;
			x16 <<= 16;
			x8 <<= 8;
			return x24 | x16 | x8 | x0;

		}
	}
}
