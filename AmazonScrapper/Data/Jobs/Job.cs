using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AmazonScrapper.Web;

namespace AmazonScrapper.Data.Jobs
{
    public class Job : IJob, IResult<string>
    {
        public List<string> PersonNames { get; } = new List<string>();

        public string Result => this.ToString();

        public void Add(string name)
        {
            PersonNames.Add(name);
        }

        public override string ToString()
        {
            string ret = string.Empty;
            foreach (var person in PersonNames)
            {
                if (string.IsNullOrEmpty(person))
                    continue;

                if (string.IsNullOrEmpty(ret))
                    ret = person;
                else
                    ret += ", " + person;
            }
            return ret;
        }
    }
}
