using System.Collections.Generic;
using PoqAssignment.Domain.Builders.Interfaces;
using PoqAssignment.Domain.Models;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Builders
{
    public class ProductsStatisticsDirector : IProductsStatisticsDirector
    {
        private IProductsStatisticsBuilder ProductsStatisticsBuilder { get; set; }

        public void CreateBuilder(IEnumerable<Product> products)
        {
            ProductsStatisticsBuilder = new ProductsStatisticsBuilder();
            ProductsStatisticsBuilder.SetProducts(products);
        }

        public ProductsStatistics BuildProductsStatistics()
        {
            return ProductsStatisticsBuilder.WithMinPrice()
                .WithMaxPrice()
                .WithSizes()
                .WithCommonWords()
                .Build();
        }
    }
}