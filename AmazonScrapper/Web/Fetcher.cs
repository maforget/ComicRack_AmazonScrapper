using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Web;
using System.Drawing;
using System.Threading;
using AmazonScrapper.Tools;

namespace AmazonScrapper.Web
{
    public class Fetcher
    {
        CookieContainer cookieContainer = new CookieContainer();
        string userAgent = string.Empty;

        public Fetcher()
        {
            cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("lc_acbfr", "en_GB") { Domain = $".amazon.{TLD.GetDescription()}" }); // Always use English
			userAgent = string.Empty;
        }

        static Fetcher _instance;
        public static Fetcher Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Fetcher();
                return _instance;
            }
        }

        private TLDs? _tld;
        public TLDs TLD
        {
            get => _tld ?? TLDs.com;
            set => _tld = value;
        }

        public void RegisterTLDs(TLDs tld)
        {
            TLD = tld;
            switch (tld)
            {
                case TLDs.fr:
                    cookieContainer.Add(new Cookie("x-amz-captcha-1", string.Empty) { Domain = ".amazon.fr" });
                    cookieContainer.Add(new Cookie("x-amz-captcha-2", string.Empty) { Domain = ".amazon.fr" });
                    cookieContainer.Add(new Cookie("i18n-prefs", "EUR") { Domain = ".amazon.fr" });
                    break;
                case TLDs.com:
                    cookieContainer.Add(new Cookie("i18n-prefs", "USD") { Domain = ".amazon.com" });
                    break;
                case TLDs.de:
                    cookieContainer.Add(new Cookie("i18n-prefs", "EUR") { Domain = ".amazon.de" });
                    break;
                case TLDs.es:
                    cookieContainer.Add(new Cookie("i18n-prefs", "EUR") { Domain = ".amazon.es" });
                    break;

            }
        }

        public void Reset()
        {
            _instance = new Fetcher();
        }

        public HtmlNode GetHtmlDocument(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                var html = ReadURL(url, ct);
                if (html.Contains("you're not a robot") || html.Contains("vous n'êtes pas un robot"))
                {
                    CaptchaDetected();
                    return null;
                }

                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc?.DocumentNode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public HtmlNode GetBody(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                return GetHtmlDocument(url, ct)?.SelectSingleNode("//body");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Stream Fetch(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest Req = WebRequest.CreateHttp(url);
                CookieContainer cookieContainer = GetCookieContainer(url);
                Req.CookieContainer = cookieContainer;
                Req.Timeout = 15000;
                Req.UserAgent = GetRandomUserAgent();
                Req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                Req.Referer = url;
                Req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
                Req.Referer = GetReferer();
                Req.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                Req.KeepAlive = true;
                WebResponse webresponse = Req.GetResponse();
                var cookies = cookieContainer.GetCookies(new Uri(url));

                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                var inStream = webresponse.GetResponseStream();

                return inStream;
            }
            catch (WebException e) when (e.InnerException is System.Net.Sockets.SocketException)
            {
                var inEx = e.InnerException as System.Net.Sockets.SocketException;
                if (inEx != null && inEx.SocketErrorCode.ToString() == "AccessDenied")
                    AccessDeniedDetected();

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetReferer()
        {
            switch (TLD)
            {
                case TLDs.com:
                    return @"https://www.amazon.com/kindle-dbs/comics-store/home";
                default:
                    return $@"https://www.amazon.{TLD.GetDescription()}";
            }
        }

        private CookieContainer GetCookieContainer(string url)
        {
            var domain = new Uri(url).Host.Replace("www", "");
            return cookieContainer;
        }

        public string ReadURL(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                var inStream = Fetch(url, ct);
                var encode = Encoding.GetEncoding("utf-8");
                var ReadStream = new StreamReader(inStream, encode);
                var page = ReadStream.ReadToEnd();

                inStream.Close();
                ReadStream.Close();

                return page;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public Image GetImage(string cover, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(cover))
                    return null;

                var stream = Fetch(cover, ct);
                var image = Image.FromStream(stream);
                return image;

                //using (WebClient web = new WebClient())
                //{
                //    var b = web.DownloadData(cover);
                //    var image = b.byteArrayToImage();
                //    return image;
                //}
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void CaptchaDetected()
        {
            System.Windows.Forms.MessageBox.Show("Captcha Detected");
        }

        private static void AccessDeniedDetected()
        {
            System.Windows.Forms.MessageBox.Show("Access Denied");
        }

        private string GetRandomUserAgent()
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                Random rand = new Random();
                object syncLock = new object();

                var browserType = new string[] { "chrome", "edge", "firefox" };
                lock (syncLock)
                {
                    //Amazon seems to block version older than 2 years
                    int currentVersion = CalculateCurrentMajorVersion();
                    int version = rand.Next(currentVersion - 16, currentVersion + 3);
                    string finalVersion = version.ToString();
                    int patch = rand.Next(1264, 2535);
                    int build = rand.Next(10, 80);
                    string randomBroswer = browserType[rand.Next(browserType.Length)];

                    var OS = new string[] { "Windows NT 10.0; Win64; x64", "Macintosh; Intel Mac OS X 13_2_1" };
                    string OSsystem = OS[rand.Next(OS.Length)];

                    var UATemplate = new Dictionary<string, string>
                    {
                        { "chrome", $"Mozilla/5.0 ({OSsystem}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{finalVersion}.0.0.0 Safari/537.36" },
                        { "edge", $"Mozilla/5.0 ({OSsystem}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{finalVersion}.0.0.0 Safari/537.36 Edg/{version}.0.{patch}.{build}" },
                        { "firefox", $"Mozilla/5.0 ({OSsystem}; rv:{finalVersion}.0) Gecko/20100101 Firefox/{finalVersion}.0" },
                    };
                    userAgent = UATemplate[randomBroswer];
                }

                SimpleLogger.Info(userAgent);
            }

            return userAgent;
        }

		private int CalculateCurrentMajorVersion()
		{
            //This is based on Firefox releases, Chrome version pretty much matches those of Firefox
            DateTime baseDate = new DateTime(2023, 07, 04);
            int baseVersion = 115;
            int numOfWeeksPerVersion = 4;

            TimeSpan timeElapsedSince = DateTime.Now.Subtract(baseDate);
            double weeksElapsed = timeElapsedSince.TotalDays / 7;

            int numOfVersionBumps = (int)Math.Round(weeksElapsed / numOfWeeksPerVersion);
            return baseVersion + numOfVersionBumps;
		}
	}
}
