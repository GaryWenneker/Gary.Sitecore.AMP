using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

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

        /// <summary>The use rewrite for AMP.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The use rewrite for AMP.</returns>
        public static bool UseAmp(this HttpContextBase context)
        {
            Assert.ArgumentNotNull((object)context, nameof(context));

            HttpRequestBase request = context.Request;
            string filePath = request.FilePath;

            filePath = filePath.TrimEnd('/');
            string pattern = @"(/)";
            var last = Regex.Split(filePath, pattern);
            var result = last.GetUpperBound(0);

            return last[last.GetUpperBound(0)].Equals("amp", StringComparison.OrdinalIgnoreCase);
        }

        public static bool UseAmp(string filePath)
        {
            filePath = filePath.TrimEnd('/');
            string pattern = @"(/)";
            var last = Regex.Split(filePath, pattern);
            var result = last.GetUpperBound(0);

            return last[last.GetUpperBound(0)].Equals("amp", StringComparison.OrdinalIgnoreCase);
        }

        public static void SetAmpCookie(this HttpContextBase context, bool expires = false)
        {
            if(expires)
                context.Request.Cookies.Remove("AMP");
            else
            {
                HttpCookie ampCookie = new HttpCookie("AMP");
                ampCookie["Font"] = "Arial";
                ampCookie["Color"] = "Blue";
                ampCookie.Expires = DateTime.Now.AddDays(1d);
                context.Response.Cookies.Add(ampCookie);
            }
        }

        public static bool GetAmpCookie(this HttpRequestBase request)
        {
            return request.Cookies["AMP"] != null;
        }

        /// <summary>Rewrites the URL after removing the language prefix.</summary>
        /// <param name="context">The context.</param>
        public static void RewriteUrl(this HttpContextBase context)
        {
            Assert.ArgumentNotNull((object)context, nameof(context));

            HttpRequestBase request = context.Request;
            string filePath = request.FilePath;

            filePath = filePath.TrimEnd('/').RemoveEnd("/amp");

            context.RewritePath(filePath);
            //context.Response.Redirect(urlString.ToString(), true);
        }
    }
}