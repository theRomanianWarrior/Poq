using System.Collections.Generic;
using System.Linq;
using PoqAssignment.Domain.Abstractions;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Filters
{
    public class SizeFilter : BaseDataProcessor
    {
        private Size? _size;

        public SizeFilter(Size? size)
        {
            _size = size;
        }

        public override IEnumerable<Product> Process(IEnumerable<Product> data)
        {
            if (_size.HasValue)
            {
                var filteredData = data.Where(p => p.Sizes.Contains((Size) _size));
                return base.Process(filteredData);
            }

            return base.Process(data);
        }
    }
}