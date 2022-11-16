using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterINI
{

    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class IniSectionAttribute : Attribute
    {
        public IniSectionAttribute(string sectionName)
        {
            this.SectionName = sectionName;
        }

        public string SectionName { get; }
    }
}
