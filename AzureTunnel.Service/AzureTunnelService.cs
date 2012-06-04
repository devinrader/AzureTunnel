using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace AzureTunnel.Service
{
    public class AzureTunnelService : IAzureTunnelService
    {
        public string Relay(TwilioRequestViewModel vm)
        {
            StringBuilder paramList = new StringBuilder();
            var props = vm.GetType().GetProperties();
            foreach (var prop in props)
            {
                string param = string.Format("{0}={1}&", prop.Name, prop.GetValue(vm, new object[] { }));
                paramList.Append(param);
            }

            HttpWebRequest request = null;
            switch (vm.Method)
            {
                case "POST":
                    request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:46622/Sms/Index");
                    request.Method = vm.Method;

                    var requeststream = request.GetRequestStream();

                    byte[] bytes = System.Text.ASCIIEncoding.Default.GetBytes(paramList.ToString());
                    requeststream.Write(bytes, 0, bytes.Length);
                    break;

                case "GET":
                    request = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}?{1}", "http://localhost:46622/Sms/Index", paramList.ToString()));
                    request.Method = vm.Method;
                    break;
            }

            var response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string result = String.Empty;
                try
                {
                    result = reader.ReadToEnd();
                }
                catch (Exception exc) {
                    Debug.WriteLine(exc.Message);
                }

                Debug.WriteLine(result);

                return result;
            }



        }
    }
}
//Console.WriteLine("{0,13}|{1,13}", vm.From, vm.To);