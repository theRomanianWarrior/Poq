using System.Collections.Generic;
using System.Linq;
using PoqAssignment.Domain.Models;

namespace PoqAssignment.Application.DTO
{
    public class MockyDto
    {
        public MockyDto(IEnumerable<ProductDto> products, ProductFilter filter)
        {
            Products = products.ToList();
            Filter = filter;
        }

        public IEnumerable<ProductDto> Products { get; set; }
        public ProductFilter Filter { get; set; }
    }
}