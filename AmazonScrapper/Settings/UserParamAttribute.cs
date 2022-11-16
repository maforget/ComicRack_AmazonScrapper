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
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string displayText;

        public UserParamAttribute(string displayText)
        {
            this.displayText = displayText;
        }

        public string DisplayText
        {
            get { return displayText; }
        }
    }
}
