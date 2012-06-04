using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using AzureTunnel;

namespace AzureTunnel.Client.Web.Controllers
{
    public class RelayController : Controller
    {
        //
        // GET: /Relay/

        public ActionResult Index(TwilioRequestViewModel vm)
        {            
            var cf = new ChannelFactory<IAzureTunnelServiceChannel>("relay");
            using (var ch = cf.CreateChannel())
            {
                vm.Method = Request.HttpMethod;

                string result = ch.Relay(vm);
                return Content(result, "application/xml");
            }            
        }
    }
}
