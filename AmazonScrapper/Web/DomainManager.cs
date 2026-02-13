using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AmazonScrapper.Tools;
using AmazonScrapper.Web;
using AmazonScrapper.Web.Searcher;
using System.Windows.Documents;

namespace AmazonScrapper.Data.Parser
{
	public class DomainManager<T> : BaseManager<T> where T : IDomain
	{
		public DomainManager(TLDs tld, params object[] parameters)
			: base(tld, parameters)
		{
            Fetcher.Instance.RegisterTLDs(tld);
        }

        protected override bool HasSubclass(T x) => x.GetType().BaseType != typeof(object);

		public T Get()
		{
			return list.FirstOrDefault();
		}
	}
}
