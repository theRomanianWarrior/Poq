using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PoqAssignment.Application.Services;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Models.Filters;
using PoqAssignment.Domain.Models.MockyIo;
using Xunit;

namespace PoqAssignment.Tests.Application.Unit
{
    public class FiltersServiceShould
    {
        [Fact]
        public void Apply_Filters_Chain_Correctly()
        {
            // Arrange
            var userFilter = new UserFilter
            {
                MinPrice = 20.0m,
                MaxPrice = 50.0m,
                Size = Size.Medium,
                Highlight = new List<Highlight> {Highlight.Red}
            };

            var products = new List<Product>
            {
                new Product
                {
                    Price = 20.0m,
                    Sizes = new List<Size> {Size.Medium},
                    Description = "This is a red product."
                },
                new Product
                {
                    Price = 30.0m,
                    Sizes = new List<Size> {Size.Large},
                    Description = "This is a blue product."
                },
                new Product
                {
                    Price = 15.0m,
                    Sizes = new List<Size> {Size.Small, Size.Medium},
                    Description = "This product has no color."
                }
            };

            var expectedResult = products.First();
            expectedResult.Description = expectedResult.Description.Replace("red", "<em>red</em>");

            var filtersService = new FiltersService();
            filtersService.CreateFiltersOptionsChain(userFilter);

            // Act
            var result = filtersService.ApplyFiltersOptions(products);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(1);
            result.First().Price.Should().Be(products.First().Price);
            result.First().Should().BeEquivalentTo(expectedResult);
        }
    }
}