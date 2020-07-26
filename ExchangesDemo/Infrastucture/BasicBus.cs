using System;
using System.Text;
using Infrastucture.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastucture
{
    public class BasicBus : IBus, IDisposable
    {
        private readonly IModel channel;
        private readonly IConnection connection;

        private readonly NamesConfigurator namesConfigurator;

        public BasicBus()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            this.connection= factory.CreateConnection();
            this.channel = connection.CreateModel();
            namesConfigurator = new NamesConfigurator();
        }     
        public void Subscribe(string exchange, Action<string> action)
        {
            channel.ExchangeDeclare(exchange, "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender,ea) => {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                action(message);
            };

            channel.BasicConsume(queueName, true, consumer);
        }

        
        public void Subscribe<T>(Action<T> action)
        {
            var exchange = namesConfigurator.GenerateExchangeName(typeof(T));

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender,ea) => {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                //TODO: Create Object of type T, from the received message
                //TODO: Use newtownsoft json;
                action(message);
            };

            channel.BasicConsume(queueName, true, consumer);
        }

        public void Publish(string exchange, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            this.channel.ExchangeDeclare(exchange, "fanout");
            this.channel.BasicPublish(exchange, "", null, body);
        }

        public void Dispose()
        {
            System.Console.WriteLine("disposing");
            this.channel.Close();
            this.connection.Close();
        }
    }
}