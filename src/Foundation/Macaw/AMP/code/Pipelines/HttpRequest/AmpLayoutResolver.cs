using Foundation.Macaw.AMP.Extensions;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Linq;
using System.Web;

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


            // Get the default device
            HttpRequestBase request = args.HttpContext.Request;
            if (ExtensionMethods.GetAmpCookie(request))
            {
                SetAmp();
            }
        }

        
        private void SetAmp()
        {
            DeviceRecords devices = Sitecore.Context.Item?.Database.Resources.Devices;
            if (devices != null)
            {
                DeviceItem defaultDevice = devices?.GetAll()?.Where(d => d.Name.ToLower() == "amp").FirstOrDefault();
                if (defaultDevice != null)
                {
                    Sitecore.Context.Device = defaultDevice;
                }
            }
        }
    }
}