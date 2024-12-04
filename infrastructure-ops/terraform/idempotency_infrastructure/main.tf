resource "random_pet" "name_prefix" {
  prefix = var.name_prefix
  length = 1
}

# create the resource group
module "resource_group" {
  source      = "../modules/resource_group"
  location    = var.location
  name_prefix = var.name_prefix
}

# create the servicebus namespace
module "servicebus_namespace" {
  source              = "../modules/servicebus_namespace"
  resource_group_name = module.resource_group.name
  location            = module.resource_group.location
  name_prefix         = var.name_prefix
  depends_on          = [module.resource_group]
}

# create the servicebus queue
module "queue" {
  source               = "../modules/queue"
  resource_group_name  = module.resource_group
  location             = var.location
  name_prefix          = var.name_prefix
  partitioning_enabled = true
  namespace_id         = module.servicebus_namespace.id
  depends_on           = [module.servicebus_namespace]
}
