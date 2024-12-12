
namespace consumer
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAll();
        Task<bool> Create(Contract contract);
        Task<bool> Update(Contract contract);
    }
}