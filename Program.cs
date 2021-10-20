using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListenForIntergrationEvents();
            CreateHostBuilder(args).Build().Run();
        }

        public static void ListenForIntergrationEvents()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "172.17.0.3",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[X] Received {0}", message);
                var type = ea.RoutingKey;
                if (type == "user.login")
                {
                    Console.WriteLine("Logged in");
                }
            };
            channel.BasicConsume(queue: "client.userservice", autoAck: true, consumer: consumer);
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
