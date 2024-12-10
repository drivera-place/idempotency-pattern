
using consumer.DataAccess.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace consumer
{
    public class ContractRepository : IContractRepository
    {
        private readonly ContractDBContext _dbContext;

        public ContractRepository(ContractDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Contract contract)
        {
            //var tmp = _dbContext.Contracts.Add(contract);
            //_dbContext.SaveChanges();

            string insertStatement = "INSERT INTO contracts (contractid, name) VALUES (@p0, @p1) ON CONFLICT (contractid) DO NOTHING;";
            _dbContext.Database.ExecuteSqlRaw(insertStatement, contract.ContractId, contract.Name);

            return true;
        }


        public Task<IEnumerable<Contract>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Contract contract)
        {
            throw new NotImplementedException();
        }
    }
}