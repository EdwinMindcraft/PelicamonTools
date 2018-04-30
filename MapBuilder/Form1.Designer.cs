namespace MapBuilder {
	partial class Form1 {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tilesetPalette1 = new MapBuilder.Controls.TilesetPalette();
			this.SuspendLayout();
			// 
			// tilesetPalette1
			// 
			this.tilesetPalette1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tilesetPalette1.AutoScroll = true;
			this.tilesetPalette1.Location = new System.Drawing.Point(12, 12);
			this.tilesetPalette1.Name = "tilesetPalette1";
			this.tilesetPalette1.RenderSize = 32;
			this.tilesetPalette1.Selected = -1;
			this.tilesetPalette1.Size = new System.Drawing.Size(244, 591);
			this.tilesetPalette1.TabIndex = 0;
			this.tilesetPalette1.Tileset = null;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1162, 615);
			this.Controls.Add(this.tilesetPalette1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.TilesetPalette tilesetPalette1;
	}
}

