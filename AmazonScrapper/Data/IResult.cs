using AmazonScrapper.Data.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data
{
    internal interface IResult<T>
    {
        T Result { get; }
    }
}
