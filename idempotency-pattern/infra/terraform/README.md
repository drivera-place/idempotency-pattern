# Module for deploying Azure Service Bus Queue

## Prerequisites

You may need to register some resource providers in your Azure Subscription before run script for the first time:

- `Microsoft.ServiceBus`
- `Microsoft.Web`
- `Microsoft.Storage`
- `microsoft.insights`
- `Microsoft.OperationalInsights`

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

Clean up resources

```bash
terraform plan -destroy -out main.destroy.tfplan
```

Applying destroy

**Warning!** the following script will destroy your resources:

```bash
# terraform apply "main.destroy.tfplan"
```

Destroying specific resources, in case you need it !Warning

**Warning!** the following script will destroy your resources:

```bash
# terraform destroy -target module.producer_function
```
