resource "azurerm_linux_function_app" "consumer" {
  name                       = "${var.name_prefix}-consumer-function-app"
  location                   = var.location
  resource_group_name        = var.resource_group_name
  storage_account_name       = var.storage_account_name
  storage_account_access_key = var.storage_account_access_key
  service_plan_id            = var.service_plan_id
  app_settings = {
    FUNCTIONS_WORKER_RUNTIME       = "dotnet-isolated"
    SCM_DO_BUILD_DURING_DEPLOYMENT = "false"
    WEBSITE_RUN_FROM_PACKAGE       = 1
    ServiceBusConnection           = var.service_bus_connection_string
  }
  site_config {
    application_stack {
      dotnet_version              = "8.0"
      use_dotnet_isolated_runtime = true
    }
    cors {
      allowed_origins = ["https://portal.azure.com"]
    }
    application_insights_connection_string = var.application_insights_connection_string
    application_insights_key               = var.application_insights_key
  }
}