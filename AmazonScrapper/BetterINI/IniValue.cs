using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterINI
{
    public class IniValue
    {
        public string Value { get; set; }
        public string Section { get; set; }

        public IniValue(string value, string section)
        {
            Value = value;
            Section = section;
        }
    }
}
