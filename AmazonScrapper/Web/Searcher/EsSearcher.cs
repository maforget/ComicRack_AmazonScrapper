namespace AmazonScrapper.Web.Searcher
{
	public class EsSearcher : Searcher
	{
		protected override int MaxPages => 2;

		public override TLDs TLD => TLDs.es;

		protected EsSearcher(string searchTerm, bool sortByDate = false, bool strictSearch = false) :
			base(searchTerm, sortByDate, strictSearch)
		{

		}

		protected override string BuildURL(bool sortByDate)
		{
			string sort = sortByDate ? "&s=date-desc-rank" : string.Empty;
			return $@"https://www.amazon.es/s?k={SearchTermEncoded}&i=stripbooks{sort}";
		}
	}
}
