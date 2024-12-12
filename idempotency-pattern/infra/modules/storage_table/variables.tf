variable "name_prefix" {
  default = "tmp"
}

variable "location" {
  default = "westus"
}

variable "storage_account_name" {
}

variable "partition_key" {
  default = "idempotencykey"
}

variable "row_key" {
  default = "message_hash_key"
}
