using System;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Queues;
using RabbitMQ;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using Serilog;

namespace SecondUser
{
    public class Program
    {
        public static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                })
                .ConfigureLogging((hostingContext, logging) => { logging.AddSerilog(dispose: true); });

            await builder.RunConsoleAsync();
                            

            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();


            channel.QueueDeclare(queue: QueueNames.CoolQueue,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Thread.Sleep(15000);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
                Console.WriteLine(message);
            };


            channel.BasicConsume(QueueNames.CoolQueue, autoAck: false, consumer);

            Console.ReadLine();
            channel.Close();
            connection.Close();
        }
    }
}
