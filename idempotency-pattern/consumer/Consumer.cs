using System.Text.Json;
using Azure.Data.Tables;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace consumer
{
    public class Consumer
    {
        private readonly ILogger<Consumer> _logger;
        private readonly IContractRepository _contractRepository;

        public Consumer(ILogger<Consumer> logger, IContractRepository contractRepository)
        {
            _logger = logger;
            _contractRepository = contractRepository;
        }

        [Function(nameof(Consumer))]
        [TableOutput("pocstidempotency", Connection = "StorageAccConnectionString")]
        public async Task<ProcessedMessageEntity> Run(
            [ServiceBusTrigger("poc-servicebus-queue", Connection = "ServiceBusConnection", AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            [TableInput("pocstidempotency","Processed", Connection = "StorageAccConnectionString")]
            TableClient idempotencyTable,
            FunctionContext context)
        {
            string partitionKey = "Exception";
            Contract newContract = new(message.MessageId, string.Empty);

            try
            {
                newContract = DeserializeMessage(message);

                var foundContract = idempotencyTable.Query<ProcessedMessageEntity>(x => x.ContractId == newContract.ContractId).FirstOrDefault();

                if (foundContract == null)
                {
                    // Payload processing logic.
                    var response = await _contractRepository.Create(newContract);
                    partitionKey = "Processed";
                }
                else
                {
                    partitionKey = "Discarded";
                    _logger.LogInformation("Message processed and disposed, nothing to do because of duplicated message.");
                }

                await messageActions.CompleteMessageAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return new ProcessedMessageEntity(newContract.ContractId, newContract.Name)
            {
                PartitionKey = partitionKey
            };
        }

        public Contract DeserializeMessage(ServiceBusReceivedMessage message)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
            var body = message.Body;

            _logger.LogInformation("Message Body: {body}", body);

            var contract = JsonSerializer.Deserialize<Contract>(body);
            _logger.LogInformation("ContractId: {contract.ContractId}", contract.ContractId);
            _logger.LogInformation("Name: {contract.Name}", contract.Name);

            return contract;
        }
    }
}
