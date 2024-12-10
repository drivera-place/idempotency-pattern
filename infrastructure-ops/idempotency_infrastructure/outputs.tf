output "resource_group_name" {
  value = module.resource_group.name
}

output "location" {
  value = module.resource_group.location
}

output "servicebus_namespace_name" {
  value = module.servicebus_namespace.name
}

output "servicebus_queue_name" {
  value = module.queue.name
}

output "producer_function_name" {
  value = module.producer_function.name
}

output "consumer_function_name" {
  value = module.consumer_function.name
}

output "postgresql_server_name" {
  value = module.postgresql_flexible_server.server_name
}

output "database_name" {
  value = module.database.name
}

output "postgresql_server_admin_password" {
  sensitive = true
  value     = module.postgresql_flexible_server.administrator_password
}

output "postgresql_server_host" {
  value = module.postgresql_flexible_server.server_fqdn
}

output "postgresql_server_administrator_login" {
  value = var.administrator_login
}

output "storage_account_access_key" {
  sensitive = true
  value = module.storage_account.primary_access_key
}

output "idempotency_storage_table" {
  value = module.idempotency_storage_table.storage_table_name
}
