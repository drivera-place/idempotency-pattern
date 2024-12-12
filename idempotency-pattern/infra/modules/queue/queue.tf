resource "azurerm_servicebus_queue" "this" {
  name                 = "${var.name_prefix}-servicebus-queue"
  namespace_id         = var.namespace_id
}
