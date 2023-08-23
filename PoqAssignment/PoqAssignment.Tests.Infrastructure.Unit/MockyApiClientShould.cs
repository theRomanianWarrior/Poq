using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using PoqAssignment.Domain.Models.MockyIo;
using PoqAssignment.Infrastructure;
using PoqAssignment.Tests.Infrastructure.Unit.Helper;
using Xunit;

namespace PoqAssignment.Tests.Infrastructure.Unit
{
    public class MockyApiClientShould
    {
        private readonly Fixture _fixture;

        public MockyApiClientShould()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Return_MockyProducts()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("mockyTest.json")
                .Build();
            
            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<MockySettings>(configuration.GetSection("MockySettings"))
                .BuildServiceProvider();

            var mockySettings = serviceProvider.GetRequiredService<IOptions<MockySettings>>().Value;
            
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            var settings = Substitute.For<IOptions<MockySettings>>();
            settings.Value.Returns(mockySettings);

            var mocky = _fixture.Create<Mocky>();

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            var content = JsonSerializer.Serialize(mocky);
            httpResponseMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient(new MockyHttpMessageFakeHandler(httpResponseMessage));
            httpClient.BaseAddress = new Uri(mockySettings.BaseUrl);
            httpClientFactory.CreateClient(mockySettings.MockyApiClient).Returns(httpClient);

            MockyApiClient.Initialize(httpClientFactory, settings);

            // Act
            var result = MockyApiClient.Instance.GetMockyProducts();

            // Assert
            result.Should().BeEquivalentTo(mocky);
        }
    }
}
