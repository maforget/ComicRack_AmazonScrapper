using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonScrapper.Data
{
    public class SerieInfo
    {
        public string RawText { get; set; }
        public string Serie { get; set; }
        public int Count { get; set; } = -1;
        public string Number { get; set; }
        public string DisplayText => $"{Serie} ({(!string.IsNullOrEmpty(Number) && Count > 0 ? $"{Number} of " : "")}{(Count > 0 ? $"{Count} " : "")}book series)";
        public AmazonLinkSerie SerieLink { get; private set; }

        /// <summary>
        /// Used When Parsing from either the search result or the book page
        /// </summary>
        /// <param name="text">the text of the series header (ex: Book 1 of 10: Serie)</param>
        /// <param name="bookTitle">the title of the book</param>
        /// <returns>a SeriesInfo</returns>
        public static SerieInfo Parse(string text, string bookTitle = "", AmazonLinkSerie amazonLinkSerie = null)
        {
            SerieInfo serieInfo = new SerieInfo();
            serieInfo.SerieLink = amazonLinkSerie;

            if (string.IsNullOrEmpty(text))
                return null;

            serieInfo.RawText = text;
			Match regex = GetSeriesRegex(text);

            string serie = regex?.Groups["title"]?.Value?.Trim();
            serieInfo.Serie = serie;

            string number = Parser.Number.ParseFromTitle(bookTitle);//Take the number from the title
            string altNumber = regex?.Groups["number"]?.Value?.Trim();//Take the number in series title
            serieInfo.Number = string.IsNullOrEmpty(number) ? altNumber?.TrimStart('0') : number?.TrimStart('0');//use the series title if no book number in title

            string count = regex?.Groups["count"]?.Value?.Trim();
            if (int.TryParse(count, out int c))
                serieInfo.Count = c;

            return serieInfo;
        }

		private static Match GetSeriesRegex(string text)
		{
            var regexPatterns = new[]
            {
			    //Expecting Pattern: "Book 5 of 19: Farmhand"
			    @"Book\s+(?<number>\d+)\s+of\s+(?<count>\d+)\:\s+(?<title>.+)",
			    //Expecting Pattern: "Part of: Usagi Yojimbo Saga (10 Books)"
			    //Expecting Pattern: "Part of: The Way of the Househusband"
			    @"Part\s+of\:\s*(?<title>(?:[^(]|$)+)(?:\((?<count>\d+)\sBooks\))*",
			    //Expecting Pattern: "Chapter 9 of 9: What I Love About You"
			    @"Chapter\s+(?<number>\d+)\s+of\s+(?<count>\d+)\:\s+(?<title>.+)",
			    //Expecting Pattern: "Volume 24 of 24: One-Punch Man"
			    @"Volume\s+(?<number>\d+)\s+of\s+(?<count>\d+)\:\s+(?<title>.+)",
			    //Excepting Pattern: "Related to: My Hero Academia"
			    @"Related\s+to\:\s*(?<title>(?:[^(]|$)+)(?:\((?<count>\d+)\sBooks\))*",
                //Expecting Pattern: "Livre 4 sur 14 : Achille Talon - L'Intégrale "
			    @"Livre\s+(?<number>\d+)\s+sur\s+(?<count>\d+)\s*\:\s+(?<title>.+)",
            };

			Match regex = null;
			foreach (var pattern in regexPatterns)
			{
				var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
				if (match.Success)
				{
					regex = match;
					break;
				}
			}

            return regex;
		}

		/// <summary>
		/// Used when Parsing from the Collection Page
		/// </summary>
		/// <param name="index">the book number</param>
		/// <param name="title">the series title</param>
		/// <param name="count">the number of books int he series</param>
		/// <returns>a SeriesInfo</returns>
		public static SerieInfo Parse(string index, string title, string count, AmazonLinkSerie amazonLinkSerie = null)
        {
            SerieInfo serieInfo = new SerieInfo();
            serieInfo.SerieLink = amazonLinkSerie;

            if (!string.IsNullOrEmpty(title))
                serieInfo.Serie = title;

            if (!string.IsNullOrEmpty(index))
                serieInfo.Number = index?.TrimStart('0');

            if (!string.IsNullOrEmpty(count) && int.TryParse(count, out int c))
                serieInfo.Count = c;

            return serieInfo;
        }
    }
}
