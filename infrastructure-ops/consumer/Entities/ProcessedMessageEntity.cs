using Azure;
using Azure.Data.Tables;

public class ProcessedMessageEntity : ITableEntity
{
    public string Hash { get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}