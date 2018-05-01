using System;
using MapBuilder.Tiles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapBuilder.Controls;

namespace MapBuilder.Controls
{
    public partial class TilemapDesigner : UserControl
    {

        public int RenderSize { get; set; } = 32;
        private Image _currentTile;
        public Image currentTile
        {
            get
            {
                return _currentTile;
            }
            set
            {
                _currentTile = value;
            }
        }

        public TilemapDesigner()
        {
            InitializeComponent();
        }

        private void TilemapDesigner_MouseClick(object sender, MouseEventArgs e)
        {

        }
        public void TileSelectEventTrigerred(int id, Tileset sender)
        {
            Console.Out.WriteLine("FROM EVENT : " + id.ToString());
            try
            {
                this.currentTile = sender.Tiles[id];
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("La tile sélectionnée est en dehors des limites du tileset.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            panel1.Invalidate();

        }
        public TilesetPalette.TileSelectEvent TileSelectEventHandler()
        {
            return TileSelectEventTrigerred;
        }

        private void TilemapDesigner_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (currentTile != null)
            {
                e.Graphics.DrawImage(currentTile, new Rectangle(2, 2, RenderSize, RenderSize));
            }
        }
    }
}
