using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace consumer
{
    public class Contract
    {
        public string ContractId { get; set; }

        public string Name { get; set; }

        public Contract(string contractId, string name)
        {
            ContractId = contractId;
            Name = name;
        }
    }
}