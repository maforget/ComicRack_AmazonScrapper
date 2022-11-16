using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser
{
    public interface IParser
    {
        HtmlNode Node { get; }
        object Parse();
        string ToString();
        int ToInt();
        float ToFloat();
        DateTime ToDateTime();
    }

    public interface IParserPage : IParser
    {

    }

    public interface IParserSearch : IParser
    {

    }

    public interface IParserCollection : IParser
    {

    }
}