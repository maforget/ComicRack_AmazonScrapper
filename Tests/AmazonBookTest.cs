using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Web;
using AmazonScrapper.Data;

namespace Tests
{
    [TestClass]
    public class AmazonBookTest
    {
        [TestMethod]
        public void TestGetBook()
        {
            var link = new AmazonLinkIssues("B07CV2SK5Z");
            var book = link.ScrapeData();

            Assert.AreEqual("Farmhand #1", book.Title);
            Assert.AreEqual("Jedidiah Jenkins is a farmer—but his cash crop isn’t corn or soy. Jed grows fast-healing, plug-and-play human organs. Lose a finger? Need a new liver? He’s got you covered. Unfortunately, strange produce isn’t the only thing Jed’s got buried. Deep in the soil of the Jenkins Family Farm, something dark has taken root, and it’s beginning to bloom. From ROB GUILLORY, Eisner-winning co-creator and artist of Image Comics’ CHEW, comes a new dark comedy about science gone sinister and agriculture gone apocalyptic. Nature is a Mother.", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B07CV2SK5Z].", book.Notes);
            Assert.AreEqual("Farmhand", book.Series);
            Assert.AreEqual("1", book.Number);
            Assert.AreEqual(34, book.PageCount);
            Assert.AreEqual(11, book.Day);
            Assert.AreEqual(7, book.Month);
            Assert.AreEqual(2018, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Image", book.Publisher);
            //Assert.AreEqual(4.1f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07CV2SK5Z", book.Web);
            Assert.AreEqual("Rob Guillory", book.Writer);//Author
            Assert.AreEqual("Rob Guillory, Taylor Wells", book.CoverArtist);
            Assert.AreEqual("Rob Guillory, Taylor Wells", book.Penciller);//Artists
            Assert.AreEqual(-1f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook2()
        {
            var link = new AmazonLinkIssues("B07HYKS19R");
            var book = link.ScrapeData();

            Assert.AreEqual("Farmhand Vol. 1", book.Title);
            Assert.AreEqual("Jedidiah Jenkins is a simple farmer. But his cash crop isn’t corn or soy. He grows fast-healing, highly-customizable human organs. For years, Jed’s organic transplants have brought healing to many, but deep in the soil of the Jenkins Family Farm something sinister has taken root. Today this dark seed will begin to sprout, and the Jenkins family will be the first to taste its bitter fruit. Collects FARMHAND #1-5", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B07HYKS19R].", book.Notes);
            Assert.AreEqual("Farmhand", book.Series);
            Assert.AreEqual("1", book.Number);
            Assert.AreEqual(149, book.PageCount);
            Assert.AreEqual(16, book.Day);
            Assert.AreEqual(1, book.Month);
            Assert.AreEqual(2019, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Image", book.Publisher);
            //Assert.AreEqual(4.7f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07HYKS19R", book.Web);
            Assert.AreEqual("Rob Guillory", book.Writer);//Author
            Assert.AreEqual("Rob Guillory", book.CoverArtist);
            Assert.AreEqual("Rob Guillory, Taylor Wells", book.Penciller);//Artists
            Assert.AreEqual(7.49f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook3()
        {
            var link = new AmazonLinkIssues("B09898B3K5");
            var book = link.ScrapeData();

            Assert.AreEqual("Usagi Yojimbo Saga Volume 3 (Second Edition)", book.Title);
            Assert.AreEqual("Stan Sakai’s epic series continues in the third volume of the definitive Usagi Yojimbo compilations featuring brand new original cover art by Stan Sakai! Usagi faces a terrifying new foe who wears a demon mask, the Eisner-winning “Grasscutter” storyline receives a sequel, pickpocket Kitsune’s history is revealed, fan-favorite character Sasuké the Demon Queller makes his debut, and a beloved ally long thought dead returns! Collects Usagi Yojimbo Volume Three #31–#52, along with stories from Dark Horse Presents and more!", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B09898B3K5].", book.Notes);
            Assert.AreEqual("Usagi Yojimbo Saga", book.Series);
            Assert.AreEqual("3", book.Number);
            //Assert.AreEqual(616, book.PageCount);
            Assert.AreEqual(4, book.Day);
            Assert.AreEqual(1, book.Month);
            Assert.AreEqual(2022, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Dark Horse Books", book.Publisher);
            //Assert.AreEqual(4.8f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B09898B3K5", book.Web);
            Assert.AreEqual("Stan Sakai", book.Writer);//Author
            Assert.AreEqual("Stan Sakai", book.Penciller);//Illustrator
            Assert.AreEqual(13.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook4()
        {
            var link = new AmazonLinkIssues("B07X5N41P8");
            var book = link.ScrapeData();

            Assert.AreEqual("Farmhand T01 (French Edition)", book.Title);
            Assert.AreEqual("Jedidiah Jenkins est agriculteur, mais il ne cultive que des organes humains « plug-andplay » à croissance rapide capable de réparer les corps. Perdre un doigt ? Besoin d'un nouveau foie ? Il a ce qu'il faut. Malheureusement, les étranges substances qu'il utilise ont quelques effets secondaires. Dans les profondeurs du sol de la ferme familiale Jenkins, quelque chose d'effrayant a pris racine et commence à grandir.", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B07X5N41P8].", book.Notes);
            Assert.AreEqual("Farmhand", book.Series);
            Assert.AreEqual("1", book.Number);
            Assert.AreEqual(160, book.PageCount);
            Assert.AreEqual(4, book.Day);
            Assert.AreEqual(9, book.Month);
            Assert.AreEqual(2019, book.Year);
            Assert.AreEqual("fr", book.LanguageISO);
            Assert.AreEqual("Delcourt", book.Publisher);
            //Assert.AreEqual(4.6f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07X5N41P8", book.Web);
            Assert.AreEqual("Rob Guillory", book.Writer);//Author
            Assert.AreEqual("Rob Guillory", book.Penciller);//Contributor
            Assert.AreEqual(11.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook5()
        {
            var link = new AmazonLinkIssues("B07VVTGY3Z");
            var book = link.ScrapeData();

            Assert.AreEqual("Batman/Superman (2019-) #2", book.Title);
            Assert.AreEqual("The Batman Who Laughs’ plot is bigger than either the Caped Crusader or the Man of Steel realized. Following a showdown with the devious killer’s first sentinel, a jacked-up, Dark Multiverse-infected Shazam!, the pair has to figure out who else has been targeted for similar transformations. Their first two guesses: someone very close to Batman and the one hero that would make failure nearly impossible-Superman himself!", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B07VVTGY3Z].", book.Notes);
            Assert.AreEqual("Batman/Superman", book.Series);
            Assert.AreEqual("2", book.Number);
            Assert.AreEqual(23, book.PageCount);
            Assert.AreEqual(25, book.Day);
            Assert.AreEqual(9, book.Month);
            Assert.AreEqual(2019, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("DC", book.Publisher);
            //Assert.AreEqual(4.6f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07VVTGY3Z", book.Web);
            Assert.AreEqual("Joshua Williamson", book.Writer);//Author
            Assert.AreEqual("David Marquez", book.Penciller);
            Assert.AreEqual("David Marquez, Alejandro Sanchez", book.CoverArtist);
            Assert.AreEqual("David Marquez", book.Inker);
            Assert.AreEqual("Alejandro Sanchez", book.Colorist);
            Assert.AreEqual(3.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook6()
        {
            var link = new AmazonLinkIssues("B081NXVJSX");
            var book = link.ScrapeData();

            Assert.AreEqual("Witchblade Collected Edition Vol. 1", book.Title);
            Assert.AreEqual("This first collection of the bestselling series created by MARC SILVESTRI, DAVID WOHL, BRIAN HABERLIN, and MICHAEL TURNER equips streetwise cop Sara Pezzini with the mysterious Witchblade, a weapon of prehistoric origin and untold power. As the artifact's bearer, Sara goes toe to toe with a Machiavellian industrialist, supernatural serial killers, and far worse, as the supernatural underworld of New York alters the course of her destiny forever. Gorgeously rendered and painstakingly assembled as the first in a series of absolute collected editions. When all eight volumes are collected, a special piece of cross-volume connecting spine art by STJEPAN SEJIC will be revealed. Collects WITCHBLADE #1-19, THE DARKNESS #9 & 10, TALES OF THE WITCHBLADE #1/2 & 3", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B081NXVJSX].", book.Notes);
            Assert.AreEqual("Witchblade", book.Series);
            Assert.AreEqual("1", book.Number);
            Assert.AreEqual(552, book.PageCount);
            Assert.AreEqual(25, book.Day);
            Assert.AreEqual(3, book.Month);
            Assert.AreEqual(2020, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Image - Top Cow", book.Publisher);
            //Assert.AreEqual(4.7f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B081NXVJSX", book.Web);
            Assert.AreEqual("Warren Ellis, Brian Haberlin, David Wohl, Christina Z., David Finch", book.Writer);//Author
            Assert.AreEqual("Michael Turner", book.CoverArtist);
            Assert.AreEqual("David Finch, Michael Turner, Marc Silvestri, Tony Salvador Daniel, Billy Tan", book.Penciller);
            Assert.AreEqual("Nathan Cabrera", book.Colorist);
            Assert.AreEqual(17.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook7()
        {
            var link = new AmazonLinkIssues("B01DUTBP8S");
            var book = link.ScrapeData();

            Assert.AreEqual("Superman: Reign of the Supermen (Superman: The Death of Superman)", book.Title);
            Assert.AreEqual(@"SUPERMAN IS DEAD.But now, four mysterious beings appear--allwith the powers and abilities of the Man of Steel! One claims he is aclone from the DNA of Superman. Another--half-man and half-machine--says he is Superman with a cyborg body. Still another, a cold redeemer ofjustice, states that he alone has the right to wear the ""S"" shield. And, finally, an armored figure who says he fights with the heart and soulof Superman.Who is the true Superman?DAN JURGENS (SUPERMAN: LOIS & CLARK), KARL KESEL (SUPERBOY), JERRY ORDWAY (ADVENTURES OF SUPERMAN), LOUISE SIMONSON (SUPERMAN: THE MAN OF STEEL) and ROGER STERN (ACTION COMICS) introduce four new Supermen to the DC Universe. Thethird of four volumes chronicling the epic saga of the Death and Returnof Superman, collecting ACTION COMICS #687-688, ADVENTURES OF SUPERMAN#500-502, SUPERMAN #78-79, SUPERMAN ANNUAL #5, SUPERMAN: THE MAN OFSTEEL #22-23 and SUPERMAN: THE MAN OF STEEL ANNUAL #2!", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B01DUTBP8S].", book.Notes);
            Assert.AreEqual("Superman: The Death of Superman", book.Series);
            //Assert.AreEqual("3", book.Number);
            Assert.AreEqual(325, book.PageCount);
            Assert.AreEqual(5, book.Day);
            Assert.AreEqual(4, book.Month);
            Assert.AreEqual(2016, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("DC; Illustrated edition", book.Publisher);
            //Assert.AreEqual(4.7f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B01DUTBP8S", book.Web);
            Assert.AreEqual("Dan Jurgens, Roger Stern, Louise Simonson, Karl Kesel, Gerard Jones", book.Writer);//Author
            Assert.AreEqual("Tom Grummett, Jackson Guice, Jon Bogdanove, M.D. Bright", book.Penciller);
            Assert.AreEqual(14.74f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook8()
        {
            var link = new AmazonLinkIssues("B09SNVRZBQ");
            var book = link.ScrapeData();

            Assert.AreEqual("One-Punch Man, Vol. 24", book.Title);
            Assert.AreEqual(@"The dangerous monster that can’t be cut, Evil Mineral Water, confronts Bushi Drill, Okama Itachi, and Iaian. Meanwhile, Saitama runs into the giant demonic dog Pochi, and Terrible Tornado launches into an epic supernatural battle against Gyoro-Gyoro!", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B09SNVRZBQ].", book.Notes);
            Assert.AreEqual("One-Punch Man", book.Series);
            Assert.AreEqual("24", book.Number);
            Assert.AreEqual(1, book.Day);
            Assert.AreEqual(11, book.Month);
            Assert.AreEqual(2022, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("VIZ Media: SHONEN JUMP", book.Publisher);
            //Assert.AreEqual(4.9f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B09SNVRZBQ", book.Web);
            Assert.AreEqual("ONE", book.Writer);//Author
            Assert.AreEqual("Yusuke Murata", book.Penciller);
            Assert.AreEqual(6.49f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook9()
        {
            var link = new AmazonLinkIssues("B09MSR9FW4");
            var book = link.ScrapeData();

            Assert.AreEqual("We Ride Titans #1", book.Title);
            Assert.AreEqual(@"Kaiju hit hard. Family hits harder.Trying to keep your family from imploding is a tall order. Kit Hobbs is about to find out it's an even taller order when that family has been piloting the Titan that protects New Hyperion from kaiju for generations. Between a spiraling brother, a powder keg of a father, and a whole bunch of twenty-story monsters, she's got her work cut out for her.", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B09MSR9FW4].", book.Notes);
            Assert.AreEqual("We Ride Titans", book.Series);
            Assert.AreEqual("1", book.Number);
            Assert.AreEqual(12, book.Day);
            Assert.AreEqual(1, book.Month);
            Assert.AreEqual(2022, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Vault Comics", book.Publisher);
            //Assert.AreEqual(4.3f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B09MSR9FW4", book.Web);
            Assert.AreEqual("Tres Dean", book.Writer);//Author
            Assert.AreEqual("Nathan Gooden", book.CoverArtist);
            Assert.AreEqual("Sebastian Piriz", book.Penciller);
            Assert.AreEqual(1.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook10()
        {
            var link = new AmazonLinkIssues("B0CQLL8R8H");
            var book = link.ScrapeData();

            Assert.AreEqual("I Hate Fairyland Vol. 6", book.Title);
            Assert.AreEqual(@"Death by a thousand Gerts! King Cloudeus is determined to finish what his sister Queen Cloudia never could: kick Gert out of Fairyland FOR GOOD! And the only way to do that is have every Gert who ever lived battle each other to the death. Who said there's only so many Gerts to go around?! They clearly haven't read this comic. OG Gert returns to find the last key to escape, but is it too late, and will she become a Lifetime Citizen of Fairyland? Eisner Award-winning writer SKOTTIE YOUNG (MIDDLEWEST, TWIG, THE ME YOU LOVE IN THE DARK) and artist BRETT BEAN (Marvel’s Rocket and Groot) are back in the triumphant return to I HATE FAIRYLAND! Collects I HATE FAIRYLAND (2022) #6-10", book.Summary);
            Assert.AreEqual("Scraped metadata from Amazon [B0CQLL8R8H].", book.Notes);
            Assert.AreEqual("I Hate Fairyland", book.Series);
            Assert.AreEqual("6", book.Number);
            Assert.AreEqual(17, book.Day);
            Assert.AreEqual(1, book.Month);
            Assert.AreEqual(2024, book.Year);
            Assert.AreEqual("en", book.LanguageISO);
            Assert.AreEqual("Image", book.Publisher);
            //Assert.AreEqual(4.3f, book.CommunityRating);
            Assert.AreEqual(@"https://www.amazon.com/dp/B0CQLL8R8H", book.Web);
            Assert.AreEqual("Skottie Young", book.Writer);//Author
            Assert.AreEqual("Skottie Young", book.Penciller);//Artist
            Assert.AreEqual("Brett B Bean", book.CoverArtist);
            Assert.AreEqual(13.99f, book.BookPrice);
        }

        [TestMethod]
        public void TestGetBook11()
        {
            var link = new AmazonLinkIssues("1779524692");
            var book = link.ScrapeData();

            Assert.AreEqual(27, book.Day);
            Assert.AreEqual(2, book.Month);
            Assert.AreEqual(2024, book.Year);
        }

		[TestMethod]
		public void TestGetBookFr()
		{
			var link = new AmazonLinkIssues("2374081044", tld: TLDs.fr);
			var book = link.ScrapeData();

            Assert.AreEqual("Les Petits Marsus et la grande ville", book.Title);

			Assert.AreEqual("Chaud Benjamin", book.Writer);
			Assert.AreEqual("Chaud Benjamin", book.Penciller);

			Assert.AreEqual(24, book.Day);
			Assert.AreEqual(8, book.Month);
			Assert.AreEqual(2018, book.Year);
            Assert.AreEqual("fr", book.LanguageISO);
            Assert.AreEqual("LITTLE URBAN; Illustrated édition", book.Publisher);
		}

		[TestMethod]
		public void TestGetBookFr2()
		{
			var link = new AmazonLinkIssues("2205060694", tld: TLDs.fr);
			var book = link.ScrapeData();

			Assert.AreEqual("Achille Talon - Intégrales - Tome 1 - Mon Oeuvre à moi - tome 1", book.Title);
            Assert.AreEqual("Achille Talon - L'Intégrale", book.Series);
            Assert.AreEqual("Le principe est simple : rééditer sous formes d'épais volumes - chaque titre agrémenté de pages d'introduction étant composé de trois albums - quelques-uns des plus beaux classiques de la bande dessinée. Ne résistez pas au plaisir de les redécouvrir...", book.Summary);

            Assert.AreEqual("GREG", book.Writer);
            Assert.AreEqual("GREG", book.Penciller);

			Assert.AreEqual(5, book.Day);
			Assert.AreEqual(7, book.Month);
			Assert.AreEqual(2007, book.Year);
            Assert.AreEqual("fr", book.LanguageISO);
            Assert.AreEqual("DARGAUD", book.Publisher);
		}
	}
}
