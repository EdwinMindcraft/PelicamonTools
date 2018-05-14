using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
	public class DoubleBufferedPanel : Panel {

		private const int WS_EX_COMPOSITED = 0x02000000;

		public DoubleBufferedPanel() {
			this.DoubleBuffered = true;
		}

		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				//cp.ExStyle |= WS_EX_COMPOSITED;
				return cp;
			}
		}
	}
}
