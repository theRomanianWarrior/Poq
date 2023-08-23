using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Filters;
using PoqAssignment.Domain.Models.MockyIo;
using Xunit;

namespace PoqAssignment.Tests.Application.Unit.Filters
{
    public class SizeFilterShould
    {
        private readonly Fixture _fixture;

        public SizeFilterShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Return_CorrectProducts_When_SizeFilter_IsApplied()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(5).ToList();
            var size = Size.Medium;

            products[0].Sizes = new List<Size> { Size.Small, Size.Medium };
            products[1].Sizes = new List<Size> { Size.Medium, Size.Large };
            products[2].Sizes = new List<Size> { Size.Large };
            products[3].Sizes = new List<Size> { Size.Medium };
            products[4].Sizes = new List<Size> { Size.Small };

            var sizeFilter = new SizeFilter(size);

            // Act
            var result = sizeFilter.Process(products);

            // Assert
            result.Should().HaveCount(3);
            result.Should().OnlyContain(product => product.Sizes.Contains(size));
        }

        [Fact]
        public void Return_AllProducts_When_Size_IsNull()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            var sizeFilter = new SizeFilter(null);

            // Act
            var result = sizeFilter.Process(products);

            // Assert
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void Return_NoProducts_When_Size_HasNoMatches()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(4).ToList();
            var sizeFilter = new SizeFilter(Size.Medium);
            
            products[0].Sizes = new List<Size> { Size.Small };
            products[1].Sizes = new List<Size> { Size.Small };
            products[2].Sizes = new List<Size> { Size.Small };
            products[3].Sizes = new List<Size> { Size.Small };

            // Act
            var result = sizeFilter.Process(products);

            // Assert
            result.Should().BeEmpty();
        }
    }
}