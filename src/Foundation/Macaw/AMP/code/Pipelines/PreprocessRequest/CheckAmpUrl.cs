using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Pipelines.PreprocessRequest;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Web;
using Foundation.Macaw.AMP.Extensions;

namespace Foundation.Macaw.AMP.Pipelines.PreprocessRequest
{
    public class CheckAmpUrl: PreprocessRequestProcessor
    {
        /// <summary>
        /// Adds a session variable when using AMP and removes the token when not
        /// </summary>
        /// <param name="args">the PreprocessRequestArgs</param>
        public override void Process(PreprocessRequestArgs args)
        {
            if (args == null)
                return;

            if (UseAmp(args.HttpContext))
            {
                SetAmp(args.HttpContext, true);
                RewriteUrl(args.HttpContext);
            }
            else
            {

                SetAmp(args.HttpContext, false);
            }
        }

        private static void SetAmp(HttpContextBase context, bool isAmp)
        {
            if (context.Session == null)
                return;

            string ampKey = "__UseAMP";
            if (context.Session[ampKey] == null)
                context.Session.Add(ampKey, true);
            else
                context.Session[ampKey] = isAmp;
        }

        /// <summary>Rewrites the URL after removing the language prefix.</summary>
        /// <param name="context">The context.</param>
        private static void RewriteUrl(HttpContextBase context)
        {
            HttpRequestBase request = context.Request;
            string filePath = request.FilePath;

            filePath = filePath.TrimEnd('/').RemoveEnd("/amp");

            context.RewritePath(filePath);
            //context.Response.Redirect(urlString.ToString(), true);
        }

        /// <summary>The use rewrite for AMP.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The use rewrite for AMP.</returns>
        private static bool UseAmp(HttpContextBase context)
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
    }
}