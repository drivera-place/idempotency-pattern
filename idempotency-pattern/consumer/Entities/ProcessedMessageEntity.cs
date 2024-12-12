using Azure;
using Azure.Data.Tables;

namespace consumer
{
    public class ProcessedMessageEntity : ITableEntity
    {
        public string ContractId { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;


        public string PartitionKey { get; set; } = string.Empty;

        public string RowKey { get; set; } = string.Empty;

        public DateTimeOffset? Timestamp { get; set; } = DateTimeOffset.MinValue;

        public ETag ETag { get; set; } = ETag.All;

        public ProcessedMessageEntity(string contractId, string name)
        {
            RowKey = contractId;
            ContractId = contractId;
            Name = name;
        }

        public ProcessedMessageEntity()
        {
        }
    }
}
