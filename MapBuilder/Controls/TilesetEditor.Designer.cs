namespace MapBuilder.Controls {
	partial class TilesetEditor {
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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.doubleBufferedPanel1 = new MapBuilder.Controls.DoubleBufferedPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(419, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// doubleBufferedPanel1
			// 
			this.doubleBufferedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.doubleBufferedPanel1.Location = new System.Drawing.Point(0, 0);
			this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
			this.doubleBufferedPanel1.Size = new System.Drawing.Size(256, 578);
			this.doubleBufferedPanel1.TabIndex = 2;
			this.doubleBufferedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.doubleBufferedPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.doubleBufferedPanel1_MouseClick);
			this.doubleBufferedPanel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseWheel);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.Controls.Add(this.vScrollBar1);
			this.panel1.Controls.Add(this.doubleBufferedPanel1);
			this.panel1.Location = new System.Drawing.Point(12, 39);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(273, 578);
			this.panel1.TabIndex = 3;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Location = new System.Drawing.Point(256, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 578);
			this.vScrollBar1.TabIndex = 3;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(291, 39);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(140, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Passage";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(291, 68);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(140, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Animation";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// TilesetEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(443, 629);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.comboBox1);
			this.Name = "TilesetEditor";
			this.Text = "TilesetEditor";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private DoubleBufferedPanel doubleBufferedPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}