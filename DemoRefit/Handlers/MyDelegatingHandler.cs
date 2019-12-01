using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DemoRefit.Handlers
{
    public class MyDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<MyDelegatingHandler> _logger;

        public MyDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<MyDelegatingHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                httpResponseMessage = await base.SendAsync(request, cancellationToken);
                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
                throw;
            }
            return httpResponseMessage;
        }
    }
}
