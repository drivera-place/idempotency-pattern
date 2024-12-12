# Idempotency Pattern

This repo provides examples of how to implement idempotency pattern on Azure using Service Bus, Functions and SQL Database.

## Requirements

- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-linux?pivots=apt)
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=linux%2Cazure-cli#install-the-azure-functions-core-tools)

## Deploying function code

```bash
az functionapp deployment source config-zip -g <resource_group> -n \
<app_name> --src <zip_file_path>
```

## Project structure

- [producer](./idempotency-pattern/producer/README.md) Azure Function with Service Bus output binding.
- [consumer](./idempotency-pattern/consumer/README.md) Azure Function with Service Bus queue consumer, implements ServiceBusTrigger, TableInput and TableOutput bindings.
- [infra](./idempotency-pattern/infra/terraform/README.md) Terraform project to manage Azure resources.
- [db](./idempotency-pattern/db/README.md) Table sql script.
