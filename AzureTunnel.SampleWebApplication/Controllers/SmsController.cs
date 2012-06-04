using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace AzureTunnel.SampleWebApplication.Controllers
{
    public class SmsController : TwilioController
    {
        //
        // GET: /Sms/

        public ActionResult Index()
        {
            var twiml = new TwilioResponse();
            twiml.Sms("Hello World");
            return TwiML(twiml);
        }

    }
}
