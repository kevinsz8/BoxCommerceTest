using BoxSendNotification;
using BoxSendNotification.Models;
using BoxSendNotification.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static IConnection connection;
    private static IModel channel;
    private static System.Timers.Timer timer;

    static async Task Main(string[] args)
    {
        InitializeRabbitMQ();


        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Order message received: {message}");

                var actionNotification = JsonConvert.DeserializeObject<ActionNotification>(message);
                var notification = new ConfirmOrder();
                var confirmOrder = await notification.ConfirmOrderAsync(actionNotification);


                Console.WriteLine("Notification " + confirmOrder);

            };

        channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);

        await MessageCheckAsync();


        Console.ReadKey();
    }

    private static void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.QueueDeclare("orders", exclusive: false);
    }

    private static async Task MessageCheckAsync()
    {
        while (true)
        {
            var queueName = "orders";
            var queueDeclareResponse = channel.QueueDeclarePassive(queueName);
            var messageCount = queueDeclareResponse.MessageCount;

            if (messageCount == 0)
            {
                Console.WriteLine("No messages in the queue.");
            }

            await Task.Delay(1000);
        }
    }
}
