using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PoqAssignment.Application.DTO;
using PoqAssignment.Domain.Builders;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Application.Services
{
    public class ProductsStatisticsService
    {
        private readonly ILogger<ProductsStatisticsService> _logger;
        private readonly IMapper _mapper;

        public ProductsStatisticsService(IMapper mapper, ILogger<ProductsStatisticsService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public MockyDto ApplyProductsStatistics(IEnumerable<Product> products)
        {
            var productsStatisticsDirector = new ProductsStatisticsDirector();
            productsStatisticsDirector.CreateBuilder(products);
            var productsStatistics = productsStatisticsDirector.BuildProductsStatistics();

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            _logger.LogInformation("Was applied products statistics successfully.");

            return new MockyDto(productsDto, productsStatistics);
        }
    }
}