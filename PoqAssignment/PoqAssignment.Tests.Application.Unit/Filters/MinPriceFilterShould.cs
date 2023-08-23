using System.Linq;
using AutoFixture;
using FluentAssertions;
using PoqAssignment.Domain.Filters;
using PoqAssignment.Domain.Models.MockyIo;
using Xunit;

namespace PoqAssignment.Tests.Application.Unit.Filters
{
    public class MinPriceFilterShould
    {
        private readonly Fixture fixture;
        
        public MinPriceFilterShould()
        {
            fixture = new Fixture();
        }
        
        [Fact]
        public void Return_EmptyResult_When_MinimalPrice_IsNull()
        {
            // Arrange
            var productsCount = 10;
            var products = fixture.CreateMany<Product>(productsCount).ToList();

            var minPriceFilter = new MinPriceFilter(null);

            // Act
            var result = minPriceFilter.Process(products);
            
            // Assert
            // Check if the next processor's Process method was called
            result.Count().Should().NotBe(productsCount);
            result.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(10.0)]
        [InlineData(15.0)]
        public void Return_CorrectProduct_When_ProductMatches_MinimalPrice(decimal minPrice)
        {
            // Arrange
            var products = fixture.CreateMany<Product>(2).ToArray();
            products[0].Price = minPrice + 1;
            products[1].Price = minPrice - 1;

            var minPriceFilter = new MinPriceFilter(minPrice);

            // Act
            var result = minPriceFilter.Process(products);

            // Assert
            result.Should().HaveCount(1)
                .And.ContainSingle(product => product.Price == products[0].Price);
        }
    }
}