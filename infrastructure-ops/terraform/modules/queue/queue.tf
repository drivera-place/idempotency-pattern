resource "azurerm_servicebus_queue" "this" {
  name                 = "${var.name_prefix}_servicebus_queue"
  namespace_id         = var.namespace_id
  partitioning_enabled = var.partitioning_enabled
}
