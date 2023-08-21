using PoqAssignment.Domain.Enums;

namespace PoqAssignment.Application.DTO
{
    public class FilterByUser
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public Size? Size { get; set; }
        public string Highlight { get; set; }
    }
}