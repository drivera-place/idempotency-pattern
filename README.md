# Idempotency Pattern

This repo provides examples of how to implement idempotency pattern on Azure using Service Bus, Functions and SQL Database.

## Requirements

- Azure CLI
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=linux%2Cazure-cli#install-the-azure-functions-core-tools)

## Deploying function code

```bash
az functionapp deployment source config-zip -g <resource_group> -n \
<app_name> --src <zip_file_path>

```
