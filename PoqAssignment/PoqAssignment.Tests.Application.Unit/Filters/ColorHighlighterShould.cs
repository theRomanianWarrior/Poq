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
    public class ColorHighlighterShould
    {
        private readonly Fixture _fixture;

        public ColorHighlighterShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Apply_ColorHighlighter()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            var colors = new List<Highlight> { Highlight.Red, Highlight.Blue };

            products[0].Description = "This is a red product.";
            products[1].Description = "This is a blue product.";
            products[2].Description = "This product has no color.";

            var colorHighlighter = new ColorHighlighter(colors);

            // Act
            var result = colorHighlighter.Process(products);

            // Assert
            result.Should().HaveCount(3);
            result.Should().ContainSingle(product => product.Description.Contains("<em>red</em>"))
                .And.ContainSingle(product => product.Description.Contains("<em>blue</em>"))
                .And.ContainSingle(product => !product.Description.Contains("<em>"));
        }
        
        [Fact]
        public void NotApply_AnyChanges_When_EmptyColors()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            var colors = new List<Highlight>();

            products[0].Description = "This is a red product.";
            products[1].Description = "This is a blue product.";
            products[2].Description = "This product has no color.";

            var colorHighlighter = new ColorHighlighter(colors);

            // Act
            var result = colorHighlighter.Process(products);

            // Assert
            result.Should().HaveCount(3);
            result.Should().NotContain(product => product.Description.Contains("<em>"));
        }

        [Fact]
        public void NotApply_AnyChanges_When_NoColorsMatch()
        {
            // Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            var colors = new List<Highlight> { Highlight.Green };

            products[0].Description = "This is a red product.";
            products[1].Description = "This is a blue product.";
            products[2].Description = "This product has no color.";

            var colorHighlighter = new ColorHighlighter(colors);

            // Act
            var result = colorHighlighter.Process(products);

            // Assert
            result.Should().HaveCount(3);
            result.Should().NotContain(product => product.Description.Contains("<em>"));
        }
    }
}