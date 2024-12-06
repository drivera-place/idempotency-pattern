output "resource_group_name" {
  value = module.resource_group.name
}

output "location" {
  value = module.resource_group.location
}

output "servicebus_namespace_name" {
  value = module.servicebus_namespace.name
}

output "servicebus_queue_name" {
  value = module.queue.name
}

output "producer_function_name" {
  value = module.producer_function.name
}

output "consumer_function_name" {
  value = module.consumer_function.name
}
