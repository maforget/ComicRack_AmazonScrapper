using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Settings
{
    public class AppendConfig
    {
        public bool Enabled { get; }
        public bool NewLine { get; }

        public AppendConfig(bool enabled, bool newLine)
        {
            Enabled = enabled;
            NewLine = newLine;
        }
    }
}
