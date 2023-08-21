using System.Collections.Generic;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Builders.Interfaces
{
    public interface IProductsStatisticsDirector
    {
        void CreateBuilder(IEnumerable<Product> products);
    }
}