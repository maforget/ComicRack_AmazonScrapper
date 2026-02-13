using AmazonScrapper.Data;
using AmazonScrapper.Tools;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using AmazonScrapper.Data.Parser;
using AmazonScrapper.Data.Parser.Search;
using System.Reflection;
using System.Windows.Forms;

namespace AmazonScrapper.Web
{
    public enum TLDs
    {
        com,
        fr,
        de,
        es,
        [Description("com.br")]
        br // Can't seem to be forced in English
    }
}
