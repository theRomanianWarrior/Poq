using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PoqAssignment.Application.DTO;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Domain.Models.Filters;

namespace PoqAssignment.Application.Services
{
    public class MockyProductsService
    {
        private readonly IFiltersService _filtersService;
        private readonly ILogger<MockyProductsService> _logger;
        private readonly IMapper _mapper;
        private readonly IMockyProductsRepository _mockyProductsRepository;
        private readonly ProductsStatisticsService _productsStatisticsService;

        public MockyProductsService(IMockyProductsRepository mockyProductsRepository, IFiltersService filtersService,
            IMapper mapper, ProductsStatisticsService productsStatisticsService, ILogger<MockyProductsService> logger)
        {
            _productsStatisticsService = productsStatisticsService;
            _filtersService = filtersService;
            _mockyProductsRepository = mockyProductsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public MockyDto GetAll(FilterByUser filter = null)
        {
            MockyDto productsAfterApplyingStats;

            var allProducts = _mockyProductsRepository.GetAll();

            _logger.LogInformation("Products retrieved. Applying required filters.");

            if (filter != null)
            {
                var userFilter = _mapper.Map<UserFilter>(filter);

                _filtersService.CreateFiltersOptionsChain(userFilter);
                var filteredProducts = _filtersService.ApplyFiltersOptions(allProducts.Products).ToList();
                _logger.LogInformation("User filters applied successfully.");

                productsAfterApplyingStats = _productsStatisticsService.ApplyProductsStatistics(filteredProducts);
            }
            else
            {
                productsAfterApplyingStats = _productsStatisticsService.ApplyProductsStatistics(allProducts.Products);
            }

            return productsAfterApplyingStats;
        }
    }
}