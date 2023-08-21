namespace PoqAssignment.Domain.Contracts
{
    public interface ISerializationService
    {
        string Serialize(object data);
    }
}