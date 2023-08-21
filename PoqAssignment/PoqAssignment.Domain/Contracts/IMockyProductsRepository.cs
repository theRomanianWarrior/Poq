using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Contracts
{
    public interface IMockyProductsRepository
    {
        public Mocky GetAll();
    }
}