using Foundation.Macaw.AMP.Extensions;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;

namespace Foundation.Macaw.AMP.Pipelines.HttpRequest
{
    public class AmpLayoutResolver : HttpRequestProcessor
    {
        /// <summary>
        /// Gets the layout for the page
        /// </summary>
        /// <param name="args"></param>
        public override void Process(HttpRequestArgs args)
        {

            Assert.ArgumentNotNull(args, "args");
            //if (Context.Item != null && Context.Database != null && Context.HttpContext.Session["__UseAMP"] != null)
            //{
            //    if (bool.Parse(Context.HttpContext.Session["__UseAMP"].ToString()) == true)
            //    {
            //        Item layoutItem = GetReferenceLayoutItem();
            //        if (layoutItem != null && layoutItem.Visualization.Layout != null)
            //        {
            //            Context.Page.FilePath = layoutItem.Visualization.Layout.FilePath;
            //        }
            //    }
            //}

            Item layoutItem = ExtensionMethods.GetReferenceLayoutItem();
            if (layoutItem != null && layoutItem.Visualization.Layout != null)
            {
                Context.Page.FilePath = "/Views/Amp.cshtml"; // layoutItem.Visualization.Layout.FilePath;
            }

        }
    }
}