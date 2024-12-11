variable "name_prefix" {
  default = "tmp"
}

variable "resource_group_name" {
  default = "resource_group_name"
}

variable "location" {
  default = "westus"
}

variable "administrator_password" {
  nullable = false
}

variable "public_network_access_enabled" {
  default = false
}

variable "administrator_login" {
  nullable = false
}
