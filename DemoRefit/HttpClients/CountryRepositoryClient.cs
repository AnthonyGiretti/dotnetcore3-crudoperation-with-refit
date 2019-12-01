using DemoRefit.Models;
using DemoRefit.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoRefit.HttpClients
{
    public class CountryRepositoryClient : ICountryRepositoryClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CountryRepositoryClient> _logger;

        public CountryRepositoryClient(HttpClient client, ILogger<CountryRepositoryClient> logger, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Country>> GetAsync()
        {
            try
            {
                string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Access token is missing");
                }
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                using (HttpResponseMessage response = await _client.GetAsync("/api/democrud"))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<IEnumerable<Country>>();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to run http query");
                return null;
            }
        }
    }
}