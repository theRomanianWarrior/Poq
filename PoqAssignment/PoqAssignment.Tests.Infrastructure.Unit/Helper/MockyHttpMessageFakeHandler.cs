using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PoqAssignment.Tests.Infrastructure.Unit.Helper
{
    public class MockyHttpMessageFakeHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public MockyHttpMessageFakeHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_response);
        }
    }
}