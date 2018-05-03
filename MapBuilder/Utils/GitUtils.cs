using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace MapBuilder.Utils
{
    static class GitUtils
    {
        public static GitHubClient client;
        public static void Init()
        {
            client = new GitHubClient(new ProductHeaderValue("PelicamonTools"));
            Console.Out.WriteLine("Established connection to GitHub API");
        }

        public static Release GetLatestVersion()
        {
            var releases = client.Repository.Release.GetAll("EdwinMindcraft", "PelicamonTools").Result;
            return releases[0];
        }

        public static int[] DecomposeVersion(Release rel)
        {
            string[] versionArray = rel.TagName.Split('.');
            int[] finalArr = new int[3];
            for (int i = 0; i < 3; i++)
            {
                finalArr[i] = Int32.Parse(versionArray[i]);
            }
            return finalArr;
        }

        public static int[] DecomposeVersion(string s)
        {
            string[] versionArray = s.Split('.');
            int[] finalArr = new int[3];
            for (int i = 0; i < 3; i++)
            {
                finalArr[i] = Int32.Parse(versionArray[i]);
            }
            return finalArr;
        }

        public static bool CompareVersion(int[] v1, int[] v2)
        ///<summary>
        /// Returns True if v1>v2, False otherwise
        ///</summary>
        {
            for (int i=0; i<3; i++)
            {
                if (v1[i] > v2[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
