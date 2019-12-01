using DemoRefit.Models;
using DemoRefit.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoRefit.HttpClients
{
    public class CountryRepositoryClientV2 : ICountryRepositoryClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<CountryRepositoryClient> _logger;

        public CountryRepositoryClientV2(HttpClient client, ILogger<CountryRepositoryClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetAsync()
        {
            using (HttpResponseMessage response = await _client.GetAsync("/api/democrud"))
            {
                try
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Country>>();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to read content");
                    return null;
                }
            }
        }
    }
}