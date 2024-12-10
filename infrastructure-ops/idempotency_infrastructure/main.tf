resource "random_pet" "name_prefix" {
  prefix = var.name_prefix
  length = 1
}

resource "random_password" "pass" {
  length = 20
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
  name_prefix          = var.name_prefix
  resource_group_name  = module.resource_group.name
  location             = module.resource_group.location
  partitioning_enabled = true
  namespace_id         = module.servicebus_namespace.id
  depends_on           = [module.servicebus_namespace]
}

# storage account
module "storage_account" {
  source              = "../modules/storage_account"
  name_prefix         = var.name_prefix
  resource_group_name = module.resource_group.name
  location            = module.resource_group.location
  depends_on          = [module.resource_group]
}

# service plan
module "service_plan" {
  source              = "../modules/service_plan"
  name_prefix         = var.name_prefix
  resource_group_name = module.resource_group.name
  location            = module.resource_group.location
  depends_on          = [module.resource_group]
}

# logs
module "log_analytics" {
  source              = "../modules/log_analytics"
  name_prefix         = var.name_prefix
  location            = module.resource_group.location
  resource_group_name = module.resource_group.name
  depends_on          = [module.resource_group]
}

# application insights
module "application_insights" {
  source              = "../modules/application_insights"
  name_prefix         = var.name_prefix
  location            = module.resource_group.location
  resource_group_name = module.resource_group.name
  depends_on          = [module.resource_group]
}

# producer function
module "producer_function" {
  source                                 = "../modules/producer_function"
  name_prefix                            = var.name_prefix
  location                               = module.resource_group.location
  resource_group_name                    = module.resource_group.name
  service_plan_id                        = module.service_plan.id
  storage_account_name                   = module.storage_account.name
  storage_account_access_key             = module.storage_account.primary_access_key
  application_insights_connection_string = module.application_insights.connection_string
  application_insights_key               = module.application_insights.key
  service_bus_connection_string          = module.servicebus_namespace.default_primary_connection_string
  depends_on                             = [module.servicebus_namespace]
}

# consumer function
module "consumer_function" {
  source                                 = "../modules/consumer_function"
  name_prefix                            = var.name_prefix
  location                               = module.resource_group.location
  resource_group_name                    = module.resource_group.name
  service_plan_id                        = module.service_plan.id
  storage_account_name                   = module.storage_account.name
  storage_account_access_key             = module.storage_account.primary_access_key
  application_insights_connection_string = module.application_insights.connection_string
  application_insights_key               = module.application_insights.key
  service_bus_connection_string          = module.servicebus_namespace.default_primary_connection_string
  depends_on                             = [module.servicebus_namespace]
}

# Create the flexible server
module "postgresql_flexible_server" {
  source                        = "../modules/postgresql_flexible_server"
  administrator_login           = var.administrator_login
  administrator_password        = random_password.pass.result
  name_prefix                   = var.name_prefix
  location                      = module.resource_group.location
  resource_group_name           = module.resource_group.name
  public_network_access_enabled = var.public_network_access_enabled
  depends_on                    = [module.resource_group]
}

# Create the DB
module "database" {
  source      = "../modules/database"
  name_prefix = var.name_prefix
  server_id   = module.postgresql_flexible_server.server_id
  depends_on  = [module.postgresql_flexible_server]
}

# Add firewall rule
module "firewall-rule" {
  source           = "../modules/firewall_rule"
  server_id        = module.postgresql_flexible_server.server_id
  start_ip_address = var.start_ip_address
  end_ip_address   = var.end_ip_address
  depends_on       = [module.postgresql_flexible_server]
}
