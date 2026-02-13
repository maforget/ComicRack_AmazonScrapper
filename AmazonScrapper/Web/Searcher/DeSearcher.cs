namespace AmazonScrapper.Web.Searcher
{
	public class DeSearcher : Searcher
	{
		protected override int MaxPages => 2;

		public override TLDs TLD => TLDs.de;

		protected DeSearcher(string searchTerm, bool sortByDate = false, bool strictSearch = false) :
			base(searchTerm, sortByDate, strictSearch)
		{

		}

		protected override string BuildURL(bool sortByDate)
		{
			string sort = sortByDate ? "&s=date-desc-rank" : string.Empty;
			return $@"https://www.amazon.de/s?k={SearchTermEncoded}&i=stripbooks{sort}";
		}
	}
}
