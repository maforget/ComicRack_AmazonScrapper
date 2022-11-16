using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Jobs
{
    public interface IJob
    {
        List<string> PersonNames { get; }
        string ToString();
        void Add(string name);
    }
}
