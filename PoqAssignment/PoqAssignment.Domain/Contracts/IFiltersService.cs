using System.Collections.Generic;
using PoqAssignment.Domain.Models.Filters;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Contracts
{
    public interface IFiltersService
    {
        void CreateFiltersOptionsChain(UserFilter userFilter);
        IEnumerable<Product> ApplyFiltersOptions(IEnumerable<Product> products);
    }
}