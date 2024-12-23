# Consumer function

This code contains a function with Service Bus Trigger that consumes messages and performs idempotency insert to a DB.

## Requirements

Azure CLI
.Net

## Setup

```bash
func new -n Consumer -t "Queuetrigger"
```

Add ServiceBus extensions for isolated worker process functions:

```bash
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.ServiceBus --version 5.22.0
```

## Run locally

```bash
export resource_group_name=$(cd ../infra/terraform && terraform output -raw resource_group_name) \
export servicebus_namespace_name=$(cd ../infra/terraform && terraform output -raw servicebus_namespace_name) \
export consumer_function_name=$(cd ../infra/terraform && terraform output -raw consumer_function_name) \
export host=$(cd ../infra/terraform && terraform output -raw postgresql_server_host) \
export database_name=$(cd ../infra/terraform && terraform output -raw database_name) \
export admin_login=$(cd ../infra/terraform && terraform output -raw postgresql_server_administrator_login) \
export admin_pwd=$(cd ../infra/terraform && terraform output -raw postgresql_server_admin_password) \
export storage_acc_connection_string=$(cd ../infra/terraform && terraform output -raw storage_acc_connection_string)
```

```bash
# Get ServiceBusConnection and add it to your local.settings.json:
az servicebus namespace authorization-rule keys list --resource-group $resource_group_name --namespace-name $servicebus_namespace_name --name RootManageSharedAccessKey --query primaryConnectionString -o tsv

func settings add ServiceBusConnection
```

```bash
# Add PostgreSQL Connection String it to your local.settings.json:
export postgres_connection_string="Host=$host;Username=$admin_login;Password=$admin_pwd;Database=$database_name;Ssl Mode=Require;"
echo $postgres_connection_string

func settings add PostgreSQLConnectionString
```

```bash
func start
```

```bash
# Add Storage Account Connection String it to your local.settings.json:
export storage_acc_connection_string=$(cd ../infra/terraform && terraform output -raw storage_acc_connection_string)
echo $storage_acc_connection_string

func settings add StorageAccConnectionString
```

```bash
func start
```

## Publish app to Azure

```bash
export consumer_function_name=$(cd ../infra/terraform && terraform output -raw consumer_function_name) && \
func azure functionapp publish $consumer_function_name --publish-local-settings
```
