using System.Collections.Generic;
using PoqAssignment.Domain.Enums;

namespace PoqAssignment.Domain.Models.Filters
{
    public class UserFilter
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Size? Size { get; set; }
        public List<Highlight> Highlight { get; set; }
    }
}