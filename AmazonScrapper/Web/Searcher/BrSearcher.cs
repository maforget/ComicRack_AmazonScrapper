namespace AmazonScrapper.Web.Searcher
{
	public class BrSearcher : Searcher
	{
		protected override int MaxPages => 2;

		public override TLDs TLD => TLDs.br;

		protected BrSearcher(string searchTerm, bool sortByDate = false, bool strictSearch = false) :
			base(searchTerm, sortByDate, strictSearch)
		{

		}

		protected override string BuildURL(bool sortByDate)
		{
			string sort = sortByDate ? "&s=date-desc-rank" : string.Empty;
			return $@"https://www.amazon.com.br/s?k={SearchTermEncoded}&i=stripbooks{sort}";
		}
	}
}
