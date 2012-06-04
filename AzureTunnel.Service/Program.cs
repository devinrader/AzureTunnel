using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.ServiceBus;

namespace AzureTunnel.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(AzureTunnelService));

            sh.AddServiceEndpoint(typeof(IAzureTunnelService), new NetTcpBinding(), "net.tcp://localhost:9385/relay");

            sh.AddServiceEndpoint(typeof(IAzureTunnelService),
                new NetTcpRelayBinding(),
                ServiceBusEnvironment.CreateServiceUri("sb", "**namespace**", "relay"))
                    .Behaviors.Add(new TransportClientEndpointBehavior
                    {
                        TokenProvider = TokenProvider.CreateSharedSecretTokenProvider("owner", "**secret**")
                    });

            sh.Open();

            Console.WriteLine("Press ENTER to close.\n\r");
            //Console.WriteLine("{0,13}|{1,13}", "From", "To");
            //Console.WriteLine("--------------------------");
            Console.ReadLine();

            sh.Close();
        }
    }
}
