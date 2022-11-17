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
        public AppendConfig Append { get; set; }

        public UserConfig(string key, string text, bool enabled, AppendConfig append = null)
        {
            Key = key;
            Text = text;
            Enabled = enabled;
            Append = append ?? new AppendConfig(false, false);
        }
    }
}
