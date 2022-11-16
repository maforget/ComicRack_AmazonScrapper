using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data
{
    public class CollectionInfo
    {
        public string Title { get; set; }
        public string Size { get; set; }

        public CollectionInfo(string collectionTitle, string collectionSize)
        {
            this.Title = collectionTitle;
            this.Size = collectionSize;
        }
    }
}
