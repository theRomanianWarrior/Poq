using System;

namespace PoqAssignment.Domain.Exceptions
{
    public class LoadAllMockyProductsFailedException : Exception
    {
        public LoadAllMockyProductsFailedException(string description)
            : base(description)
        {
        }
    }
}