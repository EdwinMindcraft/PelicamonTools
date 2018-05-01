using MapBuilder.Tiles;
using System;
using System.Windows.Forms;

namespace MapBuilder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Form1 FormInstance;
		public static MasterTileset MasterTileset { get; set; } = new MasterTileset(32);
		[STAThread]
        static void Main() {
			MasterTileset.AddChild(Properties.Resources.Outside, "Outside");
			MasterTileset.AddChild(Properties.Resources.PlainColors, "Plain Colors");
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormInstance = new Form1();
            Application.Run(FormInstance);
        }
    }
}