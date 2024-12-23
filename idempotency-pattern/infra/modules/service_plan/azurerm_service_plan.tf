resource "azurerm_service_plan" "this" {
  name                = "${var.name_prefix}-service-plan"
  resource_group_name = var.resource_group_name
  location            = var.location
  os_type             = var.os_type
  sku_name            = var.sku_name
}
