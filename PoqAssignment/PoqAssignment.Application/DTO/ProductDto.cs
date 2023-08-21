using System.Collections.Generic;
using PoqAssignment.Domain.Enums;

namespace PoqAssignment.Application.DTO
{
    public class ProductDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public List<Size> Sizes { get; set; }
        public string Description { get; set; }
    }
}