using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonScrapper.Web;

namespace AmazonScrapper.Tools
{
    public static class Extensions
    {
        public static string GetDescription(this TLDs tld) => Reflections.GetEnumDescription(typeof(TLDs), tld.ToString());

    }
}
