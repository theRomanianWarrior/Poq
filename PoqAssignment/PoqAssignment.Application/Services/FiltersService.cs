using System.Collections.Generic;
using PoqAssignment.Domain.Abstractions;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Domain.Filters;
using PoqAssignment.Domain.Models.Filters;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Application.Services
{
    public class FiltersService : IFiltersService
    {
        private BaseDataProcessor _baseDataProcessor;

        public void CreateFiltersOptionsChain(UserFilter userFilter)
        {
            var minPriceFilter = new MinPriceFilter(userFilter.MinPrice);
            var maxPriceFilter = new MaxPriceFilter(userFilter.MaxPrice);
            var sizeFilter = new SizeFilter(userFilter.Size);
            var highlighterFilter = new ColorHighlighter(userFilter.Highlight);

            _baseDataProcessor = minPriceFilter;
            _baseDataProcessor.SetNext(maxPriceFilter);
            maxPriceFilter.SetNext(sizeFilter);
            sizeFilter.SetNext(highlighterFilter);
        }

        public IEnumerable<Product> ApplyFiltersOptions(IEnumerable<Product> products)
        {
            return _baseDataProcessor.Process(products);
        }
    }
}