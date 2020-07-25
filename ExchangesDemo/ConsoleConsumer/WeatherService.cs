using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsoleConsumer
{
    public class WeatherService : IHostedService
    {
        private IModel channel;
        private IConnection connection;

        public WeatherService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            this.connection= factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.Subscribe("getWeather", OnWeatherGetMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.connection.Close();
            this.channel.Close();
            return Task.CompletedTask;
        }

        public void Subscribe(string exchange, Action<BasicDeliverEventArgs> action)
        {
            channel.ExchangeDeclare(exchange, "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender,ea) => {
                action(ea);
            };

            channel.BasicConsume(queueName, true, consumer);
            System.Console.WriteLine("Waiting for messages");
            var line = Console.ReadLine();
        }

        public void OnWeatherGetMessage(BasicDeliverEventArgs message)
        {
            var decodedMessage = Encoding.UTF8.GetString(message.Body.ToArray());
            Console.WriteLine(decodedMessage);
        }
    }
}