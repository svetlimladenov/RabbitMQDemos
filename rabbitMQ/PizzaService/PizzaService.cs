using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ;
using RabbitMQ.Client;
using Queues;
using RabbitMQ.Client.Events;
using System.Text;

namespace PizzaService
{
    public class PizzaService : IHostedService
    {
        public PizzaService(string hostName = "localhost")
        {
            this.connectionFactory = new ConnectionFactory();
            this.connectionFactory.HostName = hostName;
        }

        private readonly ConnectionFactory connectionFactory; 
        private IConnection connection;       
        private IModel channel;   

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.connection = this.connectionFactory.CreateConnection();
            this.channel = this.connection.CreateModel();

            this.channel.QueueDeclare(QueueNames.CoolQueue, true, false, false, null);
            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (sender, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine(message);
            };

            System.Console.WriteLine("Waiting for messages");
            this.channel.BasicConsume(QueueNames.CoolQueue, true, consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.channel.Close();
            this.connection.Close();
            return Task.CompletedTask;
        }
    }
}