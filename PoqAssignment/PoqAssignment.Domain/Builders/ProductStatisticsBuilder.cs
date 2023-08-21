using System;
using System.Collections.Generic;
using System.Linq;
using PoqAssignment.Domain.Builders.Interfaces;
using PoqAssignment.Domain.Models;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Builders
{
    public class ProductsStatisticsBuilder : IProductsStatisticsBuilder
    {
        private readonly ProductFilter _filter = new ProductFilter();
        private List<Product> _products;

        public void SetProducts(IEnumerable<Product> products)
        {
            _products = products.ToList();
        }

        public IProductsStatisticsBuilder WithMinPrice()
        {
            _filter.MinPrice = _products.Select(p => p.Price).Min();
            return this;
        }

        public IProductsStatisticsBuilder WithMaxPrice()
        {
            _filter.MaxPrice = _products.Select(p => p.Price).Max();
            return this;
        }

        public IProductsStatisticsBuilder WithSizes()
        {
            _filter.Sizes = _products.Select(product => string.Join(", ", product.Sizes)).ToList();
            return this;
        }

        public IProductsStatisticsBuilder WithCommonWords()
        {
            // Step 1: Flatten the collection of strings into individual words
            var words = _products
                .Select(p => p.Description)
                .SelectMany(sentence =>
                sentence.Split(new[] {' ', '.', ','}, StringSplitOptions.RemoveEmptyEntries));

            // Step 2: Group the words by their value
            var groupedWords = words.GroupBy(word => word.ToLower());

            // Step 3: Order the groups by count in descending order
            _filter.CommonWords = groupedWords.OrderByDescending(group => group.Count())
                .Skip(5)
                .Take(10)
                .Select(g => g.Key.Replace("<em>", "").Replace("</em>", "")) // Remove <em> and </em> tags
                .ToArray();

            return this;
        }

        public ProductFilter Build()
        {
            return _filter;
        }
    }
}