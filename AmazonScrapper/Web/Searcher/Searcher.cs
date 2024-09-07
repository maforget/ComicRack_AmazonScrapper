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
using System.Xml.Linq;

namespace AmazonScrapper.Web.Searcher
{
	public class Searcher : ISearcher
	{
		#region Fields
		private string _searchURL;
		//private string _searchURLSeries;
		private string _searchTerm;
		private bool _strictSearch;
		protected virtual int MaxPages => 5;
		#endregion

		public static ISearcher Create(string searchTerm, bool sortByDate = false, bool strictSearch = false, TLDs tld = TLDs.com)
		{
			var searcher = new DomainManager<ISearcher>(tld, searchTerm, sortByDate, strictSearch);
			return searcher.Get();
		}

		protected Searcher(string searchTerm, bool sortByDate = false, bool strictSearch = false)
		{
			if (!string.IsNullOrEmpty(searchTerm))
			{
				_strictSearch = strictSearch;
				_searchTerm = searchTerm;
				_searchURL = BuildURL(sortByDate);
				//_searchURLSeries = $@"https://www.amazon.com/s?i=comics-manga&rh=p_lbr_books_series_browse-bin:{SearchTermEncoded}{sort}"; 
			}
		}

		protected virtual string BuildURL(bool sortByDate)
		{
			string sort = sortByDate ? "&s=date-desc-rank" : string.Empty;
			return $@"https://www.amazon.com/s?k={SearchTermEncoded}&i=comics-manga{sort}";
		}

		#region Properties
		public virtual TLDs TLD => TLDs.com;
		public string SearchURL => _searchURL;
		public string SearchTerm => _searchTerm;
		public string SearchTermEncoded
		{
			get
			{
				string seperator = _strictSearch ? "\"" : "";
				return $"{seperator}{HttpUtility.UrlEncode(_searchTerm)}{seperator}";
			}
		}
		#endregion

		public BindingList<AmazonLink> GetResults(CancellationToken ct = default)
		{
			List<AmazonLink> results = new List<AmazonLink>();

			var nodes = GetResultNodes(ct);
			foreach (HtmlNode node in nodes)
			{
				if (ct.IsCancellationRequested)
					ct.ThrowIfCancellationRequested();

				var parser = new ParserManager<IParserSearch>(node, TLD);
				var amazon = parser.Get<Series>().Result;

				if (amazon != null && !string.IsNullOrEmpty(amazon.ASIN) && !results.Any(x => x.ASIN == amazon.ASIN))//Don't add doubles
					results.Add(amazon);
			}

			return results.ToSortableBindingListNatural();
		}

		public static BindingList<AmazonLink> GroupResultsBySerie(IEnumerable<AmazonLink> resultLists)
		{
			var seriesOnlyList = resultLists.Cast<AmazonLinkIssues>().Where(x => x.SerieInfo != null).GroupBy(x => x.SerieInfo.SerieLink.ASIN, y => y.SerieInfo.SerieLink).Select(x => x.First());
			return seriesOnlyList.Cast<AmazonLink>().ToSortableBindingListNatural();
		}

		private List<HtmlNode> GetResultNodes(CancellationToken ct = default)
		{
			List<HtmlNode> nodes = new List<HtmlNode>();
			for (int i = 1; i < MaxPages; i++)
			{
				if (ct.IsCancellationRequested)
					ct.ThrowIfCancellationRequested();
				//TODO: Get number of pages from site
				List<HtmlNode> page = GetPage(i, ct);
				if (page?.Count > 0)
					nodes.AddRange(page);
				else
					break;
			}

			return nodes;
		}

		private List<HtmlNode> GetPage(int page, CancellationToken ct = default)
		{
			List<HtmlNode> nodes = new List<HtmlNode>();
			if (ct.IsCancellationRequested)
				ct.ThrowIfCancellationRequested();

			string url = $"{SearchURL}&page={page}";
			HtmlNode body = Fetcher.Instance.GetBody(url, ct);

			if (body == null)
				return nodes;

			var noResultMessage = body.SelectSingleNode(@"//div[contains(@class, 'correction-messages-aps-redirect')]//span[contains(text(), 'No results for')]");
			if (noResultMessage != null)
				return nodes;

			HtmlNodeCollection nodeCollection = body?.SelectNodes(@"//div[contains(@class, 's-main-slot')]/div[@data-asin!='']");
			if (nodeCollection != null)
				nodes.AddRange(nodeCollection);

			return nodes;
		}
	}
}
