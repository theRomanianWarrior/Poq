using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PoqAssignment.Domain.Exceptions;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Infrastructure
{
    public class MockyApiClient
    {
        private static MockySettings _settings;
        private static IHttpClientFactory _httpClientFactory;

        private static MockyApiClient _instance;
        private static readonly object Lock = new object();

        private static Mocky _mocky;

        private MockyApiClient(IHttpClientFactory httpClientFactory, IOptions<MockySettings> settings)
        {
            _settings = settings.Value;
            _httpClientFactory = httpClientFactory;

            _mocky ??= LoadAllMockyProducts();
        }

        public static MockyApiClient Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                        throw new InvalidOperationException("MockyApiClient has not been initialized.");

                    return _instance;
                }
            }
        }

        public static void Initialize(IHttpClientFactory httpClientFactory, IOptions<MockySettings> settings)
        {
            lock (Lock)
            {
                _instance ??= new MockyApiClient(httpClientFactory, settings);
            }
        }

        public Mocky GetMockyProducts()
        {
            return _mocky;
        }

        private static Mocky LoadAllMockyProducts()
        {
            using var mockyApiClient = _httpClientFactory.CreateClient(_settings.MockyApiClient);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _settings.GetAllMockyProductsUrl);
            var httpResponseMessage = mockyApiClient.SendAsync(httpRequestMessage).GetAwaiter().GetResult();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentString = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<Mocky>(contentString,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

                return result;
            }

            var requestErrorMessage = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            throw new LoadAllMockyProductsFailedException(requestErrorMessage);
        }
    }
}