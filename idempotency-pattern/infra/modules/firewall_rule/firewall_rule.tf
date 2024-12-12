# Firewall IP restriction.
resource "azurerm_postgresql_flexible_server_firewall_rule" "allowed-ips" {
  name             = "allowed-ips"
  server_id        = var.server_id
  start_ip_address = var.start_ip_address # Use developer public IP
  end_ip_address   = var.end_ip_address   # Use developer public IP
}
