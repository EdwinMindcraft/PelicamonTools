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

namespace MapBuilder.SubWindows
{
    public partial class UpdateChecker : Form
    {
        public UpdateChecker()
        {
            InitializeComponent();
            //progressBar1.Style = ProgressBarStyle.Marquee;
            //progressBar1.MarqueeAnimationSpeed = 30;
            

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void UpdateChecker_Shown(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 300;
            Thread th = new Thread(FetchUpdate);
            th.Start();
            th.Join();
            progressBar1.MarqueeAnimationSpeed = 0;
            this.Close();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Seems like an error occured, please verify your connection and try again. \n If the problem still occurs, please contact EdwinMindcraft or Edern via Discord with the following error report attached. \n \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
