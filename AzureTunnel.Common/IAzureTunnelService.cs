using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Web;

namespace AzureTunnel
{
    [ServiceContract(Namespace = "urn:ps")]
    public interface IAzureTunnelService
    {
        [OperationContract]
        string Relay(TwilioRequestViewModel vm);
    }

    public interface IAzureTunnelServiceChannel : IAzureTunnelService, IClientChannel { }
}
