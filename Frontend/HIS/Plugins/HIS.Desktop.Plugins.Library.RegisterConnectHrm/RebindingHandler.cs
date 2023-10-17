using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.RegisterConnectHrm
{
    public class RebindingHandler : DelegatingHandler
    {
        private BindIPEndPoint bindHandler;
        string ipAdapterAddressessInternet;

        public RebindingHandler(IEnumerable<IPAddress> adapterAddresses, HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new WebRequestHandler())
        {
            this.ipAdapterAddressessInternet = adapterAddresses.FirstOrDefault(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            // Inventec.Common.Logging.LogSystem.Error("Ip check lay dc ip" + this.ipAdapterAddressessInternet);
            if (String.IsNullOrEmpty(this.ipAdapterAddressessInternet))
            {
                throw new ArgumentException();
            }
        }

        IPEndPoint Bind(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
        {
            IPAddress address = IPAddress.Parse(this.ipAdapterAddressessInternet);
            return new IPEndPoint(address, 0);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var sp = ServicePointManager.FindServicePoint(request.RequestUri);
            //sp.BindIPEndPointDelegate = bindHandler;
            sp.BindIPEndPointDelegate = new BindIPEndPoint(Bind);
            var httpResponseMessage = await base.SendAsync(request, cancellationToken);
            //Inventec.Common.Logging.LogSystem.Info(httpResponseMessage.ToString());
            return httpResponseMessage;
        }
    }
}
