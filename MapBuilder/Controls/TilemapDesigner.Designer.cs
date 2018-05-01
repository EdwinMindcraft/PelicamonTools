using System;
using System.Windows.Forms;

namespace MapBuilder.Controls {
	partial class TilemapDesigner {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(64, 64);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TilemapDesigner_MouseWheel);
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar1.Location = new System.Drawing.Point(0, 133);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(133, 17);
			this.hScrollBar1.TabIndex = 1;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.Location = new System.Drawing.Point(133, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 133);
			this.vScrollBar1.TabIndex = 2;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Location = new System.Drawing.Point(133, 133);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(17, 17);
			this.panel2.TabIndex = 3;
			// 
			// TilemapDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.panel1);
			this.Name = "TilemapDesigner";
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TilemapDesigner_MouseWheel);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Panel panel1;
		private HScrollBar hScrollBar1;
		private VScrollBar vScrollBar1;
		private Panel panel2;
	}
}
