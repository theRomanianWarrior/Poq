using System.Collections.Generic;
using PoqAssignment.Domain.Contracts;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Abstractions
{
    public abstract class BaseDataProcessor : IDataProcessor
    {
        private IDataProcessor _nextProcessor;

        public IDataProcessor SetNext(IDataProcessor nextProcessor)
        {
            _nextProcessor = nextProcessor;
            return nextProcessor;
        }

        public virtual IEnumerable<Product> Process(IEnumerable<Product> data)
        {
            if (_nextProcessor != null) return _nextProcessor.Process(data);
            return data;
        }
    }
}