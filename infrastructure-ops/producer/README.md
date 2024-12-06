# Producer function

This code contains a function with HTTP Trigger and ServiceBus Output binding.

## Requirements

Azure CLI
.Net

## Setup

Add ServiceBus extensions for isolated worker process functions:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.ServiceBus --version 5.22.0
```

## Run locally

```bash
export resource_group_name=$(cd ../idempotency_infrastructure && terraform output -raw resource_group_name) \
export servicebus_namespace_name=$(cd ../idempotency_infrastructure && terraform output -raw servicebus_namespace_name) \
export producer_function_name=$(cd ../idempotency_infrastructure && terraform output -raw producer_function_name)
```

```bash
# Get ServiceBusConnection and add it to your local.settings.json:
az servicebus namespace authorization-rule keys list --resource-group $resource_group_name --namespace-name $servicebus_namespace_name --name RootManageSharedAccessKey --query primaryConnectionString -o tsv

func settings add ServiceBusConnection
```

```bash
func start
```

## Testing

```bash
curl -H 'Content-Type: application/json' \
-d '{ "contract" : "0001", "user" : "drivera-place"}' \
-X POST \
http://localhost:7071/api/Producer
```

## Publish app to Azure

```bash
export producer_function_name=$(cd ../idempotency_infrastructure && terraform output -raw producer_function_name) && \
func azure functionapp publish $producer_function_name --publish-local-settings
```
