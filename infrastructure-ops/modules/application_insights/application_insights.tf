resource "azurerm_application_insights" "this" {
  name                = "${var.name_prefix}-application-insights"
  location            = var.location
  resource_group_name = var.resource_group_name
  application_type    = var.application_type
}
