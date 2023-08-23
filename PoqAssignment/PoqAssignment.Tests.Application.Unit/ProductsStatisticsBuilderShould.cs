using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using PoqAssignment.Domain.Builders;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Models.MockyIo;
using Xunit;

namespace PoqAssignment.Tests.Application.Unit
{
    public class ProductsStatisticsServiceShould
    {
        [Fact]
        public void Apply_Products_Statistics_Successfully()
        {
            var skippedFirstMostCommonWordsCount = 5;
            
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    Price = 30.0m,
                    Sizes = new List<Size> {Size.Medium},
                    Description = "1 2 3 4 5"
                },
                new Product
                {
                    Price = 20.0m,
                    Sizes = new List<Size> {Size.Large},
                    Description = "6 7 8 9 10."
                },
                new Product
                {
                    Price = 15.0m,
                    Sizes = new List<Size> {Size.Small, Size.Medium},
                    Description = "11 12 13 14 15"
                }
            };
            var productsStatisticsDirector = new ProductsStatisticsDirector();
            productsStatisticsDirector.CreateBuilder(products);
            
            // Act
            var result = productsStatisticsDirector.BuildProductsStatistics();

            // Assert
            result.MinPrice.Should().NotBeNull();
            result.MinPrice.Should().Be(products.Last().Price);
            
            result.MaxPrice.Should().NotBeNull();
            result.MaxPrice.Should().Be(products.First().Price);
            
            for (var productsCount = 0; productsCount < products.Count; productsCount++)
            {
                result.Sizes[productsCount].Should().BeEquivalentTo(string.Join(", ", products[productsCount].Sizes));
            }

            result.CommonWords.Length.Should().BeGreaterThan(0);
            result.CommonWords.First().Should().Be((skippedFirstMostCommonWordsCount + 1).ToString());
        }
    }
}