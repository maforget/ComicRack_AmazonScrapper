using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonScrapper.Data.Parser
{
    public static class Number
    {
        public static string ParseFromTitle(string title)
        {
            //Get number that don't look like dates, ie group of 4 number starting by 19 or 20
            title = title ?? string.Empty;
            var number = Regex.Match(title, @"(?!(?:[#Tv\.\s\-\[(\])]20\d{2}|[#Tv\.\s\-\[(\])]19\d{2}))[#Tv\.\s\-\[(\])](?<num>\d{1,4})", RegexOptions.IgnoreCase)?.Groups["num"]?.Value;

            return number;
        }

    }
}
