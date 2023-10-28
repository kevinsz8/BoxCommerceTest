using BoxSendNotification;
using BoxSendNotification.DataAccess;
using BoxSendNotification.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Text;
using System.Timers;

class Program
{
    private static ConnectionFactory factory = new ConnectionFactory
    {
        HostName = "localhost"
    };

    private static IConnection connection = factory.CreateConnection();
    private static IModel channel = connection.CreateModel();
    private static System.Timers.Timer timer = new System.Timers.Timer(5000);

    

    static async Task Main(string[] args)
    {

        channel.QueueDeclare("orders", exclusive: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Order message received: {message}");
            ActionNotification actionNotification = JsonConvert.DeserializeObject<ActionNotification>(message);
        };

        channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);


        await MessageCheckAsync();

        Console.ReadKey();
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


            await Task.Delay(5000); 
        }
    }
}