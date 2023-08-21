using System.Collections.Generic;
using System.Linq;
using PoqAssignment.Domain.Abstractions;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Filters
{
    public class MinPriceFilter : BaseDataProcessor
    {
        private decimal? _minPrice;

        public MinPriceFilter(decimal? minPrice)
        {
            _minPrice = minPrice;
        }

        public override IEnumerable<Product> Process(IEnumerable<Product> data)
        {
            if (_minPrice.HasValue)
            {
                var filteredData = data.Where(p => p.Price >= _minPrice);
                return base.Process(filteredData);
            }

            return base.Process(data);
        }
    }
}