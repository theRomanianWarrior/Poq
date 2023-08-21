using Microsoft.Extensions.Logging;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Infrastructure.Repositories
{
    public class MockyProductsRepository : IMockyProductsRepository
    {
        private readonly ILogger<MockyProductsRepository> _logger;
        private readonly ISerializationService _serializationService;
        private readonly MockyApiClient _mockyApiClient;

        public MockyProductsRepository(ILogger<MockyProductsRepository> logger,
            ISerializationService serializationService, MockyApiClient mockyApiClient)
        {
            _logger = logger;
            _serializationService = serializationService;
            _mockyApiClient = mockyApiClient;
        }

        public Mocky GetAll()
        {
            var result = _mockyApiClient.GetMockyProducts();
            var serializedResponse = _serializationService.Serialize(result);

            _logger.LogInformation(serializedResponse);

            return result;
        }
    }
}