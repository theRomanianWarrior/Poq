using System.Collections.Generic;
using System.Linq;
using PoqAssignment.Domain.Abstractions;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Filters
{
    public class MaxPriceFilter : BaseDataProcessor
    {
        private decimal? _maxPrice;

        public MaxPriceFilter(decimal? maxPrice)
        {
            _maxPrice = maxPrice;
        }

        public override IEnumerable<Product> Process(IEnumerable<Product> data)
        {
            if (_maxPrice.HasValue)
            {
                var filteredData = data.Where(p => p.Price <= _maxPrice);
                return base.Process(filteredData);
            }

            return base.Process(data);
        }
    }
}