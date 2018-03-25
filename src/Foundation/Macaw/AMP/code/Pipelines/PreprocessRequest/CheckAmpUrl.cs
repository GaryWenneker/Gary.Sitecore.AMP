using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.PreprocessRequest;
using System.Text.RegularExpressions;
using System;
using System.Linq;
using System.Web;
using Foundation.Macaw.AMP.Extensions;
using Sitecore.Data;
using Sitecore.Data.Items;

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
            Assert.ArgumentNotNull((object)args, nameof(args));

            // remove initial check (expired = true)
            args.HttpContext.SetAmpCookie(true);

            // check if the url is requested with the /amp postfix
            if (args.HttpContext.UseAmp())
            {
                // set cookie for future usage
                args.HttpContext.SetAmpCookie();
                //  rewrite url to get original item
                args.HttpContext.RewriteUrl();
            }
        }
    }
}