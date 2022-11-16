using BetterINI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Settings
{
    [IniSection("OtherConfig")]
    public class OtherConfig
    {
        [IniParam(Default = false)]
        public bool GroupBySerie { get; set; } = false;
    }
}
