using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Settings
{
    public class UserConfig
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public bool Enabled { get; set; } = true;

        public UserConfig(string key, string text, bool enabled)
        {
            Key = key;
            Text = text;
            Enabled = enabled;
        }
    }
}
