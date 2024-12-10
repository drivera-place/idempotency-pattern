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
        public async Task Run(
            [ServiceBusTrigger("poc-servicebus-queue", Connection = "ServiceBusConnection", AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            [TableInput("pocstidempotency","idempotencykey", Connection = "StorageAccConnectionString", Filter ="message_hash_key eq  'Hash", Take = 1)]
            ProcessedMessageEntity processedMessage)
        {
            var body = message.Body;
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var msg = JsonSerializer.Deserialize<Contract>(body);

            _logger.LogInformation("ContractId: {msg.ContractId}", msg.ContractId);
            _logger.LogInformation("ContractId: {msg.Name}", msg.Name);

            var response = await _contractRepository.Create(new Contract(msg.ContractId, msg.Name));

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
