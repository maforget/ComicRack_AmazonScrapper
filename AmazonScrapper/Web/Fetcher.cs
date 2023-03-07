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

namespace AmazonScrapper.Web
{
    public static class Fetcher
    {
        public static HtmlNode GetHtmlDocument(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                var html = ReadURL(url, ct);
                if (html.Contains("you're not a robot"))
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

        public static HtmlNode GetBody(string url, CancellationToken ct = default)
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

        public static string ReadURL(string url, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(url))
                    return null;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest Req = WebRequest.CreateHttp(url);
                Req.Timeout = 15000;
                //Req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:104.0) Gecko/20100101 Firefox/104.0";
                Req.UserAgent = GetRandomUserAgent();
                Req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                //Req.Headers.Add("X-Powered-By", "PHP/5.3.17");
                Req.Referer = url;
                Req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
                Req.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                Req.KeepAlive = true;
                WebResponse webresponse = Req.GetResponse();

                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                var inStream = webresponse.GetResponseStream();
                var encode = Encoding.GetEncoding("utf-8");
                var ReadStream = new StreamReader(inStream, encode);
                var page = ReadStream.ReadToEnd();

                inStream.Close();
                ReadStream.Close();

                //return HttpUtility.HtmlDecode(page);
                return page;
            }
            catch (WebException e) when (e.InnerException is System.Net.Sockets.SocketException)
            {
                var inEx = e.InnerException as System.Net.Sockets.SocketException;
                if (inEx != null && inEx.SocketErrorCode.ToString() == "AccessDenied")
                    AccessDeniedDetected();

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static Image GetImage(string cover, CancellationToken ct = default)
        {
            try
            {
                if (ct.IsCancellationRequested)
                    ct.ThrowIfCancellationRequested();

                if (string.IsNullOrEmpty(cover))
                    return null;

                using (WebClient web = new WebClient())
                {
                    var b = web.DownloadData(cover);
                    var image = b.byteArrayToImage();
                    return image;
                }
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

        private static string GetRandomUserAgent()
        {
            Random rand = new Random();
            object syncLock = new object();

            string userAgent = "";
            var browserType = new string[] { "chrome", "edge", "firefox" };
            lock (syncLock)
            {
                int version = rand.Next(103, 110);
                string finalVersion = version.ToString();
                int patch = rand.Next(1264, 1660);
                int build = rand.Next(10, 80);
                string randomBroswer = browserType[rand.Next(browserType.Length)];

                var OS = new string[] { "Windows NT 10.0; Win64; x64", "X11; Linux x86_64", "Macintosh; Intel Mac OS X 13_2_1" };
                string OSsystem = OS[rand.Next(OS.Length)];

                var UATemplate = new Dictionary<string, string> 
                {
                    { "chrome", $"Mozilla/5.0 ({OSsystem}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{finalVersion}.0.0.0 Safari/537.36" },
                    { "edge", $"Mozilla/5.0 ({OSsystem}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{finalVersion}.0.0.0 Safari/537.36 Edg/{version}.0.{patch}.{build}" },
                    { "firefox", $"Mozilla/5.0 ({OSsystem}; rv:{finalVersion}.0) Gecko/20100101 Firefox/{finalVersion}.0" },
                };
                userAgent = UATemplate[randomBroswer];
            }

            //System.Windows.Forms.MessageBox.Show(userAgent);
            return userAgent;
        }
    }
}
