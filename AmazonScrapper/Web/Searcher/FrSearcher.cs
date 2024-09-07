namespace AmazonScrapper.Web.Searcher
{
	public class FrSearcher : Searcher
	{
		protected override int MaxPages => 2;

		public override TLDs TLD => TLDs.fr;

		protected FrSearcher(string searchTerm, bool sortByDate = false, bool strictSearch = false) :
			base(searchTerm, sortByDate, strictSearch)
		{

		}

		protected override string BuildURL(bool sortByDate)
		{
			string sort = sortByDate ? "&s=date-desc-rank" : string.Empty;
			return $@"https://www.amazon.fr/s?k={SearchTermEncoded}&i=stripbooks{sort}";
		}
	}
}
