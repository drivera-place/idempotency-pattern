variable "name_prefix" {
  default = "tmp"
}

variable "resource_group_name" {
  default = "rg"
}

variable "location" {
  default = "westus"
}

variable "storage_account_name" {
}

variable "storage_account_access_key" {
}

variable "service_plan_id" {
}

variable "application_insights_connection_string" {
}

variable "application_insights_key" {
}

variable "service_bus_connection_string_name" {
  default = "ServiceBusConnection"
}

variable "service_bus_connection_string" {
}
