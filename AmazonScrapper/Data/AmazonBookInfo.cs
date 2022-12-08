using AmazonScrapper.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Drawing;
using AmazonScrapper.Data.Jobs;
using System.Threading;
using AmazonScrapper.Tools;
using AmazonScrapper.Data.Parser.Page;
using AmazonScrapper.Data.Parser;

namespace AmazonScrapper.Data
{
    public class AmazonBookInfo
    {
        #region Fields
        private string title = string.Empty;
        private string series = string.Empty;
        private string number = string.Empty;
        private int count = -1;
        private int volume = -1;
        private string alternateSeries = string.Empty;
        private string alternateNumber = string.Empty;
        private string storyArc = string.Empty;
        private string seriesGroup = string.Empty;
        private int alternateCount = -1;
        private string summary = string.Empty;
        private string notes = string.Empty;
        private string review = string.Empty;
        private int year = -1;
        private int month = -1;
        private int day = -1;
        private string writer = string.Empty;
        private string penciller = string.Empty;
        private string inker = string.Empty;
        private string colorist = string.Empty;
        private string letterer = string.Empty;
        private string coverArtist = string.Empty;
        private string editor = string.Empty;
        private string publisher = string.Empty;
        private string imprint = string.Empty;
        private string genre = string.Empty;
        private string web = string.Empty;
        private int pageCount;
        private string languageISO = string.Empty;
        private string format = string.Empty;
        private YesNo blackAndWhite = YesNo.Unknown;
        private MangaYesNo manga = MangaYesNo.Unknown;
        private string characters = string.Empty;
        private string teams = string.Empty;
        private string mainCharacterOrTeam = string.Empty;
        private string locations = string.Empty;
        private float communityRating;
        private string scanInformation = string.Empty;
        private string cover = string.Empty;
        private float bookPrice = -1f;
        #endregion

        #region Properties
        [DefaultValue("")]
        public string Title => title;
        [DefaultValue("")]
        public string Series => series;
        [DefaultValue("")]
        public string Number => number;
        [DefaultValue(-1)]
        public int Count => count;
        [DefaultValue(-1)]
        public int Volume => volume;
        [DefaultValue("")]
        public string AlternateSeries => alternateSeries;
        [DefaultValue("")]
        public string AlternateNumber => alternateNumber;
        [DefaultValue("")]
        public string StoryArc => storyArc;
        [DefaultValue("")]
        public string SeriesGroup => seriesGroup;
        [DefaultValue(-1)]
        public int AlternateCount => alternateCount;
        [DefaultValue("")]
        public string Summary => summary;
        [DefaultValue("")]
        public string Notes => notes;
        [DefaultValue("")]
        public string Review => review;
        [DefaultValue(-1)]
        public int Year => year;
        [DefaultValue(-1)]
        public int Month => month;
        [DefaultValue(-1)]
        public int Day => day;
        [DefaultValue("")]
        public string Writer => writer;
        [DefaultValue("")]
        public string Penciller => penciller;
        [DefaultValue("")]
        public string Inker => inker;
        [DefaultValue("")]
        public string Colorist => colorist;
        [DefaultValue("")]
        public string Letterer => letterer;
        [DefaultValue("")]
        public string CoverArtist => coverArtist;
        [DefaultValue("")]
        public string Editor => editor;
        [DefaultValue("")]
        public string Publisher => publisher;
        [DefaultValue("")]
        public string Imprint => imprint;
        [DefaultValue("")]
        public string Genre => genre;
        [DefaultValue("")]
        public string Web => web;
        public int PageCount => pageCount;
        [DefaultValue("")]
        public string LanguageISO => languageISO;
        [DefaultValue("")]
        public string Format => format;

        [DefaultValue(YesNo.Unknown)]
        public YesNo BlackAndWhite
        {
            get
            {
                return blackAndWhite;
            }
        }

        [DefaultValue(MangaYesNo.Unknown)]
        public MangaYesNo Manga
        {
            get
            {
                return manga;
            }
        }
        [DefaultValue("")]
        public string Characters => characters;
        [DefaultValue("")]
        public string Teams => teams;
        [DefaultValue("")]
        public string MainCharacterOrTeam => mainCharacterOrTeam;
        [DefaultValue("")]
        public string Locations => locations;
        [DefaultValue(-1f)]
        public float CommunityRating => communityRating;
        [DefaultValue("")]
        public string ScanInformation => scanInformation;

        private Image _cover;
        public Image Cover
        {
            get
            {
                if (_cover == null)
                    _cover = Fetcher.GetImage(cover);

                return _cover;
            }
        }

        [DefaultValue(-1f)]
        public float BookPrice => bookPrice;

        #endregion

        public static AmazonBookInfo GetAmazonBookInfo(AmazonLinkIssues link, CancellationToken ct = default)
        {
            AmazonBookInfo bookInfo = new AmazonBookInfo();

            if (ct.IsCancellationRequested)
                ct.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(link.Link))
                return null;

            HtmlNode node = Fetcher.GetBody(link.Link, ct);
            if (node == null)
                return null;

            var parser = new ParserManager<IParserPage>(node);
            bookInfo.cover = parser.Get<Cover>().Result;
            bookInfo.title = parser.Get<Title>().Result;
            bookInfo.summary = parser.Get<Summary>().Result;

            //Persons
            var jobsCollection = parser.Get<People>().Result;
            bookInfo.writer = jobsCollection.Get<Writer>().Result;
            bookInfo.penciller = jobsCollection.Get<Penciller>().Result;
            bookInfo.inker = jobsCollection.Get<Inker>().Result;
            bookInfo.colorist = jobsCollection.Get<Colorist>().Result;
            bookInfo.letterer = jobsCollection.Get<Letterer>().Result;
            bookInfo.coverArtist = jobsCollection.Get<CoverArtist>().Result;
            bookInfo.editor = jobsCollection.Get<Editor>().Result;

            var serieInfo = parser.Get<Series>().Result;
            bookInfo.series = serieInfo.Serie;
            bookInfo.count = serieInfo.Count;
            bookInfo.number = serieInfo.Number;

            //Product details section
            bookInfo.web = link.Link;
            bookInfo.notes = !string.IsNullOrEmpty(link.ASIN) ? $"Scraped metadata from Amazon [{link.ASIN}]." : string.Empty;
            bookInfo.publisher = parser.Get<Publisher>().Result;
            bookInfo.languageISO = parser.Get<Language>().Result;
            bookInfo.pageCount = parser.Get<PageCount>().Result;
            bookInfo.communityRating = parser.Get<Rating>().Result;
            bookInfo.bookPrice = parser.Get<BookPrice>().Result;

            //Date
            DateTime pubDate = parser.Get<Date>().Result;
            bookInfo.year = pubDate.Year;
            bookInfo.month = pubDate.Month;
            bookInfo.day = pubDate.Day;

            return bookInfo;
        }

        public object GetValue(string property) => this.GetType().GetPropertyValue(this, property);
        public object GetDefault(string property) => this.GetType().GetProperty(property).GetDefault();
    }

    public enum YesNo
    {
        Unknown = -1,
        No,
        Yes
    }

    public enum MangaYesNo
    {
        Unknown = -1,
        No,
        Yes,
        YesAndRightToLeft
    }
}
