resource "azurerm_servicebus_namespace" "this" {
  name                = "${var.name_prefix}-servicebus-namespace"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = var.sku

  tags = {
    source  = "terraform"
    project = "${var.name_prefix}-project"
  }
}
