using System.Collections.Generic;

namespace PoqAssignment.Domain.Models
{
    public class ProductsStatistics
    {
        public ProductsStatistics()
        {
            Sizes = new List<string>();
            CommonWords = new string[10];
        }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string> Sizes { get; set; }
        public string[] CommonWords { get; set; }
    }
}