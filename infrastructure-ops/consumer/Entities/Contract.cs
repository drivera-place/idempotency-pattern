using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer
{
    public class Contract
    {
        public int ContractId { get; set; }

        public string Name { get; set; }

        public Contract(int contractId, string name)
        {
            ContractId = contractId;
            Name = name;
        }
    }
}