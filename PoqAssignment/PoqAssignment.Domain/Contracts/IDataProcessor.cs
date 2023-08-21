using System.Collections.Generic;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Contracts
{
    public interface IDataProcessor
    {
        IDataProcessor SetNext(IDataProcessor nextProcessor);
        IEnumerable<Product> Process(IEnumerable<Product> data);
    }
}