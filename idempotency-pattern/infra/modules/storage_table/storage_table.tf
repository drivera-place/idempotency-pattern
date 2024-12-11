resource "azurerm_storage_table" "this" {
  name                 = "${var.name_prefix}stidempotency"
  storage_account_name = var.storage_account_name
}

resource "azurerm_storage_table_entity" "entity" {
  storage_table_id = azurerm_storage_table.this.id

  partition_key = var.partition_key
  row_key       = var.row_key

  entity = {
    sessionId = "000000000"
  }
}
