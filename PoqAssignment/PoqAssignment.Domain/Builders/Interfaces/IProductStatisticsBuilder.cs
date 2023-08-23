using System.Collections.Generic;
using PoqAssignment.Domain.Models;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Builders.Interfaces
{
    public interface IProductsStatisticsBuilder
    {
        void SetProducts(IEnumerable<Product> products);
        IProductsStatisticsBuilder WithMinPrice();
        IProductsStatisticsBuilder WithMaxPrice();
        IProductsStatisticsBuilder WithSizes();
        IProductsStatisticsBuilder WithCommonWords();
        ProductsStatistics Build();
    }
}