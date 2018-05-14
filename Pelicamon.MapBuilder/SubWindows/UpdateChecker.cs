using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using MapBuilder.Utils;
using Octokit;

namespace Pelicamon.MapBuilder.SubWindows
{
    public partial class UpdateChecker : Form
    {

        public delegate void UpdateFinishEvent();
        public event UpdateFinishEvent OnUpdateFinish = new UpdateFinishEvent(() => { });

        public UpdateChecker()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.PeliMakerIcon;
            this.OnUpdateFinish += ExitToMain;
            //progressBar1.Style = ProgressBarStyle.Marquee;
            //progressBar1.MarqueeAnimationSpeed = 30;
        }

        private void ExitToMain()
        {
            Invoke(new Action(() =>
            {
                this.Close();
            }
            ));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExitToMain();
        }



        private void UpdateChecker_Shown(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 10;
            Thread th = new Thread(FetchUpdate);
            th.Start();
            //th.Join();
        }

        private void FetchUpdate()
        {
            try
            {
                Release latestRelease = GitUtils.GetLatestVersion();
                int[] latestVersion = GitUtils.DecomposeVersion(latestRelease);
                int[] currentVersion = GitUtils.DecomposeVersion(MapBuilder.Program.Version);

                if (GitUtils.CompareVersion(latestVersion, currentVersion))
                {
                    DialogResult choice = MessageBox.Show("A new version is available ! (Current : " + MapBuilder.Program.Version + " | Latest : " + latestRelease.TagName + ") \n Would you like to go to the downloads page ?", "Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (choice == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/EdwinMindcraft/PelicamonTools/releases");
                    }

                }
                else
                {
                    Console.Out.WriteLine("Current version matches latest release");
                }
                OnUpdateFinish.Invoke();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seems like an error occured, please verify your connection and try again. \n If the problem still occurs, please contact EdwinMindcraft or Edern via Discord with the following error report attached. \n \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnUpdateFinish.Invoke();
                return;
            }
        }
    }
}
