using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Producer
{
    public class OutputType
    {
        [ServiceBusOutput("poc-servicebus-queue", Connection = "ServiceBusConnection")]
        public string? OutputEvent { get; set; }

        public HttpResponseData HttpResponse { get; set; }
    }
}
