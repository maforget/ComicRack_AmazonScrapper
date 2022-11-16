using HtmlAgilityPack;
using System;

namespace AmazonScrapper.Data.Parser
{
    public abstract class ParserBase : IParser
    {
        public HtmlNode Node { get; }

        public ParserBase(HtmlNode node)
        {
            Node = node;
        }

        public override string ToString()
        {
            return this.ToType<string>();
        }

        public DateTime ToDateTime()
        {
            return this.ToType<DateTime>();
        }

        public int ToInt()
        {
            return this.ToType<int>();
        }

        public float ToFloat()
        {
            return this.ToType<float>();
        }

        public abstract object Parse();
    }

    public abstract class ParserPage : ParserBase, IParserPage
    {
        protected ParserPage(HtmlNode node) : base(node)
        {
        }    
    }

    public abstract class ParserSearch : ParserBase, IParserSearch
    {
        protected ParserSearch(HtmlNode node) : base(node)
        {
        }
    }

    public abstract class ParserCollection : ParserBase, IParserCollection
    {
        protected ParserCollection(HtmlNode node) : base(node)
        {
        }
    }

    public static class Converter
    {
        public static T ToType<T>(this IParser parser)
        {
            return (T)parser.Parse();
        }
    }
}
