using MapBuilder.Controls;

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
			this.tilemapDesigner1 = new MapBuilder.Controls.TilemapDesigner();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.binaryFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.binariesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tilesetPalette1
			// 
			this.tilesetPalette1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tilesetPalette1.AutoScroll = true;
			this.tilesetPalette1.Location = new System.Drawing.Point(12, 27);
			this.tilesetPalette1.Name = "tilesetPalette1";
			this.tilesetPalette1.RenderSize = 32;
			this.tilesetPalette1.Selected = -1;
			this.tilesetPalette1.Size = new System.Drawing.Size(276, 576);
			this.tilesetPalette1.TabIndex = 0;
			// 
			// tilemapDesigner1
			// 
			this.tilemapDesigner1.ActiveLayer = -1;
			this.tilemapDesigner1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tilemapDesigner1.Location = new System.Drawing.Point(294, 27);
			this.tilemapDesigner1.Name = "tilemapDesigner1";
			this.tilemapDesigner1.RenderSize = 32;
			this.tilemapDesigner1.Selected = -1;
			this.tilemapDesigner1.Size = new System.Drawing.Size(856, 576);
			this.tilemapDesigner1.TabIndex = 1;
			this.tilemapDesigner1.Tileset = null;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1162, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsToolStripMenuItem,
            this.importFromToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exportAsToolStripMenuItem
			// 
			this.exportAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binaryFileToolStripMenuItem});
			this.exportAsToolStripMenuItem.Name = "exportAsToolStripMenuItem";
			this.exportAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exportAsToolStripMenuItem.Text = "Export as...";
			// 
			// binaryFileToolStripMenuItem
			// 
			this.binaryFileToolStripMenuItem.Name = "binaryFileToolStripMenuItem";
			this.binaryFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.binaryFileToolStripMenuItem.Text = "Binary File";
			this.binaryFileToolStripMenuItem.Click += new System.EventHandler(this.binaryFileToolStripMenuItem_Click);
			// 
			// importFromToolStripMenuItem
			// 
			this.importFromToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binariesToolStripMenuItem});
			this.importFromToolStripMenuItem.Name = "importFromToolStripMenuItem";
			this.importFromToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.importFromToolStripMenuItem.Text = "Import from...";
			// 
			// binariesToolStripMenuItem
			// 
			this.binariesToolStripMenuItem.Name = "binariesToolStripMenuItem";
			this.binariesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.binariesToolStripMenuItem.Text = "Binary File";
			this.binariesToolStripMenuItem.Click += new System.EventHandler(this.binariesToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1162, 615);
			this.Controls.Add(this.tilemapDesigner1);
			this.Controls.Add(this.tilesetPalette1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.TilesetPalette tilesetPalette1;
		private Controls.TilemapDesigner tilemapDesigner1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem binaryFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem binariesToolStripMenuItem;
	}
}

