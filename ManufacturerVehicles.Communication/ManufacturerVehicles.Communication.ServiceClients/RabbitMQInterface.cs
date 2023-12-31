﻿using ManufacturerVehicles.Communication.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ManufacturerVehicles.Communication.ServiceClients
{
    public class RabbitMQInterface : IRabbitMQInterface
    {
        public Task SendProductMessage<T>(T message)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using
            var channel = connection.CreateModel();

            channel.QueueDeclare("orders", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

            return Task.CompletedTask;
        }
    }
}