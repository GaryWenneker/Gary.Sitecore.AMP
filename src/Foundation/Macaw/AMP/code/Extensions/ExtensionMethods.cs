using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore;
using Sitecore.Data.Items;

namespace Foundation.Macaw.AMP.Extensions
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString CanonicalUrl(this HtmlHelper html, string host, string protocol)
        {
            var canonical = GetCanonicalUrl(HttpContext.Current.Request.RequestContext.RouteData, host, protocol);
            return new MvcHtmlString(canonical.ToString(TagRenderMode.SelfClosing));
        }
        public static TagBuilder GetCanonicalUrl(RouteData route, String host, string protocol)
        {
            //These rely on the convention that all your links will be lowercase! 
            string actionName = route.Values["action"].ToString().ToLower();
            string controllerName = route.Values["controller"].ToString().ToLower();
            //If your app is multilanguage and your route contains a language parameter then lowercase it also to prevent EN/en/ etc....
            //string language = route.Values["language"].ToString().ToLower();
            string finalUrl = String.Format("{0}://{1}/{2}/{3}", protocol, host, controllerName, actionName);

            var canonical = new TagBuilder("link");
            canonical.MergeAttribute("href", finalUrl);
            canonical.MergeAttribute("rel", "canonical");
            return canonical;
        }

        public static string RegexReplace(this string source, string pattern, string replacement)
        {
            return Regex.Replace(source, pattern, replacement);
        }

        public static string ReplaceEnd(this string source, string value, string replacement)
        {
            return RegexReplace(source, $"{value}$", replacement);
        }

        public static string RemoveEnd(this string source, string value)
        {
            return ReplaceEnd(source, value, string.Empty);
        }

        public static Item GetReferenceLayoutItem()
        {
            string presentationItemPath = "/sitecore/layout/Layouts/AMP/AMP";
            return Context.Database?.GetItem(presentationItemPath);
        }
    }
}