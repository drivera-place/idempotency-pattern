output "server_name" {
  value = azurerm_postgresql_flexible_server.this.name
}

output "administrator_password" {
  sensitive = true
  value     = azurerm_postgresql_flexible_server.this.administrator_password
}

output "server_id" {
  value = azurerm_postgresql_flexible_server.this.id
}

output "server_fqdn" {
  value = azurerm_postgresql_flexible_server.this.fqdn
}
