variable "name_prefix" {
  default = "tmp"
}

variable "location" {
  default = "westus"
}

variable "resource_group_name" {
  default = "rg"
}

variable "sku" {
  default = "PerGB2018"
}

variable "retention_in_days" {
  default = 30
}
