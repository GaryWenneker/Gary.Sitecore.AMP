using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeyRed.MarkdownSharp;
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

            ViewBag.mylayout = Sitecore.Context.Page.FilePath;// = layoutItem.Visualization.Layout.FilePath;




            // Create new markdown instance
            Markdown mark = new Markdown();

            // Run parser
            string text = mark.Transform(md);

            //if(true)
            //    return View("amp", new Models.BlogPost() { Content = text });
            //else
                return View(new Models.BlogPost() { Content = text });
        }
    }
}