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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormInstance = new Form1();
            Application.Run(FormInstance);
        }
    }
}