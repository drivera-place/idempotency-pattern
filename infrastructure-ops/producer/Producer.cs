using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Producer
{
    public class Producer
    {
        private readonly ILogger<Producer> _logger;

        public Producer(ILogger<Producer> logger)
        {
            _logger = logger;
        }

        [Function("Producer")]
        public async Task<OutputType> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, FunctionContext context)
        {
            _logger.LogInformation($"C# HTTP trigger function processed a request for {context.InvocationId}.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation("Message Body: {body}", requestBody);

            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);

            return new OutputType()
            {
                OutputEvent = requestBody,
                HttpResponse = response
            };
        }
    }
}
