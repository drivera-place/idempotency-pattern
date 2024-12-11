using System.Text.Json;
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
            [TableInput("pocstidempotency",Connection = "StorageAccConnectionString")]
            IEnumerable<ProcessedMessageEntity> entity,
            FunctionContext context)
        {
            var body = message.Body;
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var msg = JsonSerializer.Deserialize<Contract>(body);

            _logger.LogInformation("ContractId: {msg.ContractId}", msg.ContractId);
            _logger.LogInformation("Name: {msg.Name}", msg.Name);

            try
            {
                //_logger.LogInformation($"PK={processedMessage.PartitionKey}, RK={processedMessage.RowKey}, SessionId={processedMessage.SessionId}. ");
                _logger.LogInformation("entity: {entity.count()}", entity.Count());
                _logger.LogInformation("###");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            //_logger.LogInformation("ProcessedMessage: {processedMessage}", processedMessage);

            if (entity.Select(x => x.ContractId == message.MessageId).FirstOrDefault() == default)
            {
                var response = await _contractRepository.Create(new Contract(msg.ContractId, msg.Name));
                await messageActions.CompleteMessageAsync(message);
            }

            return new ProcessedMessageEntity(msg.ContractId.ToString(), msg.Name)
            {
                PartitionKey = "idempotencykey",
                RowKey = Guid.NewGuid().ToString()
            };
        }
    }
}
