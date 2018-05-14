using System;
using System.Windows.Forms;

namespace Pelicamon.MapBuilder.Controls {
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
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.buttonRemoveLayer = new System.Windows.Forms.Button();
			this.buttonAddLayer = new System.Windows.Forms.Button();
			this.layerList = new System.Windows.Forms.ListView();
			this.panel1 = new MapBuilder.Controls.DoubleBufferedPanel();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar1.Location = new System.Drawing.Point(0, 283);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(373, 17);
			this.hScrollBar1.TabIndex = 1;
			this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.Location = new System.Drawing.Point(373, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 283);
			this.vScrollBar1.TabIndex = 2;
			this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Location = new System.Drawing.Point(373, 283);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(17, 17);
			this.panel2.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.buttonRemoveLayer);
			this.panel3.Controls.Add(this.buttonAddLayer);
			this.panel3.Controls.Add(this.layerList);
			this.panel3.Location = new System.Drawing.Point(400, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(100, 300);
			this.panel3.TabIndex = 4;
			// 
			// buttonRemoveLayer
			// 
			this.buttonRemoveLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveLayer.Enabled = false;
			this.buttonRemoveLayer.Location = new System.Drawing.Point(3, 274);
			this.buttonRemoveLayer.Name = "buttonRemoveLayer";
			this.buttonRemoveLayer.Size = new System.Drawing.Size(94, 23);
			this.buttonRemoveLayer.TabIndex = 2;
			this.buttonRemoveLayer.Text = "Remove Layer";
			this.buttonRemoveLayer.UseVisualStyleBackColor = true;
			this.buttonRemoveLayer.Click += new System.EventHandler(this.buttonRemoveLayer_Click);
			// 
			// buttonAddLayer
			// 
			this.buttonAddLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddLayer.Location = new System.Drawing.Point(3, 246);
			this.buttonAddLayer.Name = "buttonAddLayer";
			this.buttonAddLayer.Size = new System.Drawing.Size(94, 23);
			this.buttonAddLayer.TabIndex = 1;
			this.buttonAddLayer.Text = "Add Layer";
			this.buttonAddLayer.UseVisualStyleBackColor = true;
			this.buttonAddLayer.Click += new System.EventHandler(this.buttonAddLayer_Click);
			// 
			// layerList
			// 
			this.layerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.layerList.Location = new System.Drawing.Point(0, 0);
			this.layerList.MultiSelect = false;
			this.layerList.Name = "layerList";
			this.layerList.Size = new System.Drawing.Size(100, 240);
			this.layerList.TabIndex = 0;
			this.layerList.UseCompatibleStateImageBehavior = false;
			this.layerList.View = System.Windows.Forms.View.List;
			this.layerList.SelectedIndexChanged += new System.EventHandler(this.layerList_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(64, 64);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseClick);
			this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
			this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
			this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
			this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
			this.panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TilemapDesigner_MouseWheel);
			// 
			// TilemapDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.panel1);
			this.Name = "TilemapDesigner";
			this.Size = new System.Drawing.Size(500, 300);
			this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TilemapDesigner_Scroll);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.TilemapDesigner_MouseWheel);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private HScrollBar hScrollBar1;
		private VScrollBar vScrollBar1;
		private Panel panel2;
		private DoubleBufferedPanel panel1;
		private Panel panel3;
		private ListView layerList;
		private Button buttonRemoveLayer;
		private Button buttonAddLayer;
	}
}
