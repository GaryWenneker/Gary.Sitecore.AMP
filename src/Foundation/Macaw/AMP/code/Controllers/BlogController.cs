using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Foundation.Macaw.AMP.Extensions;
using HeyRed.MarkdownSharp;
using HtmlAgilityPack;
using Sitecore.Layouts;

namespace Foundation.Macaw.AMP.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {

            var item = Sitecore.Context.Item;
            string md  = item.Fields["Content"].Value;

            // Create new markdown instance
            Markdown mark = new Markdown();

            // Run parser
            string text = mark.Transform(md);

            if (HttpContext.UseAmp())
                text = UpdateAmpImages(text);

            return View(new Models.BlogPost() { Content = text });
        }

        private string UpdateAmpImages(string response)
        {
            // Use HtmlAgilityPack (install-package HtmlAgilityPack)
            var doc = GetHtmlDocument(response);
            var imageList = doc.DocumentNode.Descendants("img");

            const string ampImage = "amp-img";

            if (!imageList.Any()) return response;

            if (!HtmlNode.ElementsFlags.ContainsKey("amp-img"))
            {
                HtmlNode.ElementsFlags.Add("amp-img", HtmlElementFlag.Closed);
            }

            foreach (var imgTag in imageList)
            {
                var original = imgTag.OuterHtml;
                var replacement = imgTag.Clone();

                replacement.Attributes["alt"]?.Remove();

                if(replacement.Attributes["layout"] == null)
                    replacement.Attributes.Add("layout", "responsive");

                if (replacement.Attributes["width"] == null)
                    replacement.Attributes.Add("width", "600");

                if (replacement.Attributes["height"] == null)
                    replacement.Attributes.Add("height", "400");



                replacement.Name = ampImage;
                response = response.Replace(original, replacement.OuterHtml);
            }

            return response;
        }

        private HtmlDocument GetHtmlDocument(string htmlContent)
        {
            var doc = new HtmlDocument
            {
                OptionOutputAsXml = true,
                OptionDefaultStreamEncoding = Encoding.UTF8
            };
            doc.LoadHtml(htmlContent);
            return doc;
        }
    }
}