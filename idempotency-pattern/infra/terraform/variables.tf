variable "name_prefix" {
  default     = "poc"
  description = "Prefix of the resource name."
}

variable "location" {
  default     = "westus"
  description = "Location of the resource."
}

variable "start_ip_address" {
  default = "0.0.0.0"
}

variable "end_ip_address" {
  default = "255.255.255.255"
}

variable "public_network_access_enabled" {
  default = true
}

variable "administrator_login" {
  default = "adminTerraform"
}
