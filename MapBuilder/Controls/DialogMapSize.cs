using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapBuilder.Controls {
	public partial class DialogMapSize : Form {

		public int MapWidth {
			get {
				if (int.TryParse(this.textBoxWidth.Text, out int r) && r > 0)
					return r;
				return defW;
			}
		}

		public int MapHeight {
			get {
				if (int.TryParse(this.textBoxHeight.Text, out int r) && r > 0)
					return r;
				return defH;
			}
		}

		private readonly int defW;
		private readonly int defH;

		public DialogMapSize(int width, int height) {
			InitializeComponent();
			this.textBoxHeight.Text = height.ToString();
			this.textBoxWidth.Text = width.ToString();
			this.defH = height;
			this.defW = width;
		}

		private void Filter_KeyPress(object sender, KeyPressEventArgs e) {
			if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
				e.Handled = true;
			if (e.KeyChar == '\n' || e.KeyChar == '\r') {
				e.Handled = true;
				this.buttonOk_Click(sender, new EventArgs());
			}
		}

		private void buttonOk_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
