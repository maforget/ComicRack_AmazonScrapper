﻿using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AmazonScrapper.Data.Jobs;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonScrapper.Data.Parser.Page
{
    public class People: ParserPage, IResult<JobsCollection>
    {

        public People(HtmlNode node) : base(node)
        {
        }

        public JobsCollection Result => this.ToType<JobsCollection>();

        public override object Parse()
        {
            JobsCollection jobsCollection = new JobsCollection();
            HtmlNodeCollection authorslNodeCollection = Node.SelectNodes(".//span[contains(@class, 'author')]");

            foreach (HtmlNode author in authorslNodeCollection)
            {
                string person1 = author.SelectSingleNode(".//a[contains(@class, 'contributorNameID')]")?.InnerText?.Trim().DecodeHTML();
                string person2 = author.SelectSingleNode(".//a[@class='a-link-normal']")?.InnerText?.Trim().DecodeHTML();//Alternative for second billed
                string authorName = string.IsNullOrEmpty(person1) ? person2 : person1; authorName = authorName ?? string.Empty;
                string jobs = author.SelectSingleNode(".//span[@class='contribution']")?.InnerText?.Trim().DecodeHTML(); jobs = jobs ?? string.Empty;
                var jobsList = ParseJobs(jobs);

                foreach (var job in jobsList)
                {
                    IJob people = null;
                    switch (job)
                    {
                        case "Author":
                            people = jobsCollection.Get<Writer>();
                            break;
                        case "Cover Art":
                            people = jobsCollection.Get<CoverArtist>();
                            break;
                        case "Artist":
                        case "Penciller":
                        case "Illustrator":
                        case "Contributor":
                            people = jobsCollection.Get<Penciller>();
                            break;
                        case "Colorist":
                            people = jobsCollection.Get<Colorist>();
                            break;
                        case "Inker":
                            people = jobsCollection.Get<Inker>();
                            break;
                        case "Editor":
                            people = jobsCollection.Get<Editor>();
                            break;
                        case "Letterer":
                            people = jobsCollection.Get<Letterer>();
                            break;
                        default:
                            people = jobsCollection.Get<Job>();
                            break;

                    }

                    people.Add(authorName);
                }
            }

            return jobsCollection;

        }

        private static List<string> ParseJobs(string jobs)
        {
            List<string> listJobs = new List<string>();

            if (string.IsNullOrEmpty(jobs))
                return listJobs;

            Regex regex = new Regex("[^(),]+", RegexOptions.IgnoreCase);
            Match results = regex.Match(jobs);
            while (results.Success)
            {
                string res = results.Value?.Trim();
                if (res.Length > 0)
                    listJobs.Add(res);

                results = results.NextMatch();
            }

            return listJobs;

        }
    }
}
