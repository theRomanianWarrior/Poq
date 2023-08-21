using Microsoft.Extensions.Logging;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Infrastructure.Repositories
{
    public class MockyProductsRepository : IMockyProductsRepository
    {
        private readonly ILogger<MockyProductsRepository> _logger;
        private readonly ISerializationService _serializationService;

        public MockyProductsRepository(ILogger<MockyProductsRepository> logger,
            ISerializationService serializationService)
        {
            _logger = logger;
            _serializationService = serializationService;
        }

        public Mocky GetAll()
        {
            var result = MockyApiClient.Instance.GetMockyProducts();
            var serializedResponse = _serializationService.Serialize(result);

            _logger.LogInformation(serializedResponse);

            return result;
        }
    }
}