output "name" {
  value = azurerm_storage_account.this.name
}

output "primary_access_key" {
  value = azurerm_storage_account.this.primary_access_key
}

output "connection_string" {
  value = azurerm_storage_account.this.primary_connection_string
}
