resource "azurerm_log_analytics_workspace" "this" {
  name                = "${var.name_prefix}-logs-workspace"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = var.sku
  retention_in_days   = var.retention_in_days

  tags = {
    environment = "dev"
    source      = "terraform"
    owner       = "drivera-place"
  }
}
