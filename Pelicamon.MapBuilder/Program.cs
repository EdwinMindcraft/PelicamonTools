using Pelicamon.Common.Tiles;
using System;
using System.Windows.Forms;
using Pelicamon.MapBuilder.SubWindows;
using Pelicamon.MapBuilder.Utils;
using Pelicamon.Common.Utils;

namespace Pelicamon.MapBuilder {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static Form1 FormInstance;
		public static MasterTileset MasterTileset { get; set; } = new MasterTileset(32);
        public static string Version = "0.0.1";
        [STAThread]
        static void Main()
        {
            #region Update Check
            GitUtils.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UpdateChecker uCheckInstance = new UpdateChecker();
            Application.Run(uCheckInstance);
            #endregion;
            #region Main Program Start
            MasterTileset.AddChild("Outside", Properties.Resources.Outside);
            MasterTileset.AddChild("Plain Colors", Properties.Resources.PlainColors);
            MasterTileset.AddChild("Manual Convert", Properties.Resources.TilesConverted);
            MasterTileset.AddChild("Size Test", 48, Properties.Resources.Tiles48);
            MasterTileset.AddChild("Poke Center", Properties.Resources.Poke_Centre_interiorfix);
			MasterTileset.AddAutoChild("Autotiles", 48, AutotileImageFormat.RMMV, Properties.Resources.PokemonfixJ2_A2);
			//MasterTileset.AddChild("Rendered Autotile", 48, ImageUtils.GenerateAutotileBitmaps(AutotileImageFormat.RMMV, ImageUtils.SubImage(Properties.Resources.Pokemon_A2, new System.Drawing.Rectangle(96*2, 0, 96, 144)), 48));
            FormInstance = new Form1();
            Application.Run(FormInstance);
            #endregion;
        }
    }
}