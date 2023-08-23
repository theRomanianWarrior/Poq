using System.Linq;
using AutoFixture;
using FluentAssertions;
using PoqAssignment.Domain.Filters;
using PoqAssignment.Domain.Models.MockyIo;
using Xunit;

namespace PoqAssignment.Tests.Application.Unit.Filters
{
    public class MaxPriceFilterShould
    {
        private readonly Fixture _fixture;

        public MaxPriceFilterShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Return_EmptyResult_When_MaxPrice_IsNull()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            var maxPriceFilter = new MaxPriceFilter(null);

            // Act
            var result = maxPriceFilter.Process(products);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Return_NoProducts_When_MaxPrice_IsZero()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(2).ToList();
            var maxPriceFilter = new MaxPriceFilter(0);

            // Act
            var result = maxPriceFilter.Process(products);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Return_CorrectProducts_When_MaxPriceFilterIsApplied()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(5).ToList();
            var maxPrice = 30.0m;
            
            products[0].Price = maxPrice + 1;
            products[1].Price = maxPrice - 1;
            products[2].Price = maxPrice + 5;
            products[3].Price = maxPrice - 5;
            products[4].Price = maxPrice;
            
            var maxPriceFilter = new MaxPriceFilter(maxPrice);

            // Act
            var result = maxPriceFilter.Process(products);

            // Assert
            result.Should().HaveCount(3);
            result.Should().OnlyContain(product => product.Price <= maxPrice);
        }
    }
}
