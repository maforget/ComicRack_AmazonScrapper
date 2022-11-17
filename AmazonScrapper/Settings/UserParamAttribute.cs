using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Settings
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class UserParamAttribute : Attribute
    {
        public string DisplayText { get; }
        public AppendConfig Append { get; }

        public UserParamAttribute(string displayText, bool append = false, bool newLine = false)
        {
            this.DisplayText = displayText;
            this.Append = new AppendConfig(append, newLine);
        }
    }
}
