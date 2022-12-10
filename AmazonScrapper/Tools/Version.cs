using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AmazonScrapper.Tools
{
    public static class Version
    {
        public static string GetCurrentVersionInfo()
        {
            var ass = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var name = ass.ProductName;
            var ver = ass.ProductVersion;

            var isDirty = !string.IsNullOrEmpty(Properties.Resources.isDirty.Trim());
            var currentCommit = $"{Properties.Resources.CurrentCommit?.Substring(0, 7)}{(isDirty ? "-dirty" : "")}";
            var currentTag = GetCurrentTag();
            var isFullRelease = !string.IsNullOrEmpty(currentTag);

            return $"{name} v{ver}{(isFullRelease ? "" : " [" + currentCommit + "]")}";
        }

        private static string GetCurrentTag()
        {
            var tags = Properties.Resources.Tags.Trim();
            var currentCommit = Properties.Resources.CurrentCommit.Trim();
            var dict = new Dictionary<string, string>();
            var sr = new StringReader(tags);

            while (true)
            {
                string line = sr.ReadLine();

                if (line != null)
                {
                    var split = line.Split(' ');
                    var commit = split[0].Trim();
                    var tag = split[1].Trim().Replace("refs/tags/", "");
                    dict.Add(tag, commit);
                }
                else
                {
                    break;
                }
            }

            return dict.FirstOrDefault(x => x.Key != "nightly" && x.Value == currentCommit).Key;
        }
    }
}
