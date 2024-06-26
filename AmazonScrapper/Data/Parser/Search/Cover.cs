﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Search
{
    public class Cover : ParserSearch, IResult<string>
    {
        public Cover(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the Search Page
        /// </summary>
        /// <param name="node">The HtmlNode for the search item</param>
        /// <returns>the cover link</returns>
        public override object Parse()
        {
            string[] imageLink = Node.SelectSingleNode(@".//img[contains(@class, 's-image')]")?.Attributes["srcset"]
                .Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (imageLink != null)
            {
                //Change the URL from webp to jpg, when using the chrome user agent.
                for (int i = 0; i < imageLink.Length; i++)
                {
                    imageLink[i] = Regex.Replace(imageLink[i], "/W/WEBP.+?/images|FMwebp_", "", RegexOptions.IgnoreCase);
                }

                return imageLink.Length == 0 ? string.Empty : imageLink[imageLink.Length - 1].Split(' ')[0]; 
            }

            return default;
        }
    }
}
