using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Foundation.Macaw.AMP.DisplayModes
{
    public class GoogleAmpDisplayMode : DefaultDisplayMode
    {
        public GoogleAmpDisplayMode() : base("amp") // for filename.amp.cshtml files.
        {
            ContextCondition = context => context.Request.RawUrl.Contains("?amp");
        }
    }
}