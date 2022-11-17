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

        public UserParamAttribute(string displayText)
        {
            this.DisplayText = displayText;
        }
    }
}
