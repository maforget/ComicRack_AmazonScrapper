using AmazonScrapper.Data;
using System.ComponentModel;
using System.Threading;

namespace AmazonScrapper.Web.Searcher
{
	public interface ISearcher: IDomain
	{
		string SearchTerm { get; }
		string SearchTermEncoded { get; }
		string SearchURL { get; }
		BindingList<AmazonLink> GetResults(CancellationToken ct = default);
	}
}
