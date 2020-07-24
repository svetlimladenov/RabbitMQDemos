using System;
using System.Text;
using RabbitMQ.Client;

namespace PizzaService 
{
    public class PizzaConsumer : DefaultBasicConsumer
    {
        public PizzaConsumer(IModel channel)
            :base(channel)
        {
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, System.ReadOnlyMemory<byte> body)
        {
            var bodyArray = body.ToArray();
            var message = Encoding.UTF8.GetString(bodyArray);
            Console.WriteLine(message);
        }

        public void Publish(string message, string queue)
        {
            var body = Encoding.UTF8.GetBytes(message);
            this.Model.BasicPublish("", queue, null, body);
        }
    }
}