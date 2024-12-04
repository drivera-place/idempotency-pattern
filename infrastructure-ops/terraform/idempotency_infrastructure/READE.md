# Module for deploying Azure Service Bus Queue

## Prerequisites

You may need to register `Microsoft.ServiceBus` resource provider before run Azure Service Bus resources

## Operations

Initialize Terraform

```bash
terraform init -upgrade
```

Create plan

```bash
terraform plan -out main.tfplan
```

Apply a Terraform execution plan

```bash
terraform apply main.tfplan
```

Verify the results

```bash

```

Clean up resources

```bash
terraform plan -destroy -out main.destroy.tfplan
```

Applying destroy !Warning

```bash
terraform apply "main.destroy.tfplan"
```
