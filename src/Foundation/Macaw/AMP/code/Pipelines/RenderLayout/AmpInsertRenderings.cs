using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Pipelines.RenderLayout;
using Foundation.Macaw.AMP.Extensions;
using System.Collections;

namespace Foundation.Macaw.AMP.Pipelines.RenderLayout
{
    public class AmpInsertRenderings : RenderLayoutProcessor
    {
        /// <summary>
        /// Adds sublayout onto the page
        /// </summary>
        /// <param name="args">
        public override void Process(RenderLayoutArgs args)
        {
            if (Context.Item != null)
            {
                if (Context.Item.Visualization.Layout == null)
                {
                    Item layoutItem = ExtensionMethods.GetReferenceLayoutItem();
                    if (layoutItem != null)
                    {
                        IEnumerable renderingReferences =
                            layoutItem.Visualization.GetRenderings(Context.Device, true);
                        foreach (RenderingReference rendering in renderingReferences)
                        {
                            Context.Page.AddRendering(rendering);
                        }
                    }
                }
            }

            
        }
    }
}