using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MOS.API.Base
{
    public class ApiForbiddenResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly string _reason;

        public ApiForbiddenResult(HttpRequestMessage request, string reason)
        {
            _request = request;
            _reason = reason;
        }

        public ApiForbiddenResult(HttpRequestMessage request)
        {
            _request = request;
            _reason = "Forbidden";
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.Forbidden, _reason);
            return Task.FromResult(response);
        }
    }
}