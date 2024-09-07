using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AmazonScrapper.Tools;
using AmazonScrapper.Web;

namespace AmazonScrapper.Data.Parser
{
    public abstract class BaseManager<T>: Collection<T>
    {
		public BaseManager(TLDs tld, params object[] args)
			: base(args)
		{
			///Filters the parsers so that it keeps either:
			///    1. A subclass that implements the IDomain interface that matches the specified TLD.
			///    2. The base parser type itself if no matching subclass is found.
			///    
			IEnumerable <IGrouping<Type, T>> grouped = list.GroupBy(x => HasSubclass(x) ? x.GetType().BaseType : x.GetType());
			var filteredList = grouped?.Select(g =>
			{
				var matchingSubclass = g.FirstOrDefault(x => x is IDomain domain && domain.TLD == tld);
				return matchingSubclass == null
					? g.First(x => x.GetType() == g.Key)
					: g.FirstOrDefault(x => x is IDomain domain && domain.TLD == tld);
			}).ToList();
			list = filteredList;
		}

		protected abstract bool HasSubclass(T x);
	}
}
