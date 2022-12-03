using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterINI;

namespace AmazonScrapper.Settings
{
    [IniSection("Fields")]
    public class Fields
    {
        [IniParam(Default = true)]
        public bool Title { get; set; } = true;

        [IniParam(Default = true)]
        public bool Series { get; set; } = true;

        [IniParam(Default = true)]
        public bool Summary { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Notes", true, true)]
        public bool Notes { get; set; } = true;

        [IniParam(Default = true)]
        public bool Number { get; set; } = true;

        [IniParam(Default = true)]
        public bool Cover { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("of Count")]
        public bool Count { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Page Count")]
        public bool PageCount { get; set; } = true;

        [IniParam(Default = true)]
        public bool Day { get; set; } = true;

        [IniParam(Default = true)]
        public bool Month { get; set; } = true;

        [IniParam(Default = true)]
        public bool Year { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Language")]
        public bool LanguageISO { get; set; } = true;

        [IniParam(Default = true)]
        public bool Publisher { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Community Rating")]
        public bool CommunityRating { get; set; } = true;

        [IniParam(Default = true)]
        public bool Writer { get; set; } = true;

        [IniParam(Default = true)]
        public bool Penciller { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Cover Artist")]
        public bool CoverArtist { get; set; } = true;

        [IniParam(Default = true)]
        public bool Inker { get; set; } = true;

        [IniParam(Default = true)]
        public bool Colorist { get; set; } = true;

        [IniParam(Default = true)]
        public bool Letterer { get; set; } = true;

        [IniParam(Default = true)]
        public bool Editor { get; set; } = true;

        [IniParam(Default = true)]
        public bool Web { get; set; } = true;

        [IniParam(Default = true)]
        [UserParam("Book Price")]
        public bool BookPrice { get; set; } = true;
    }
}
