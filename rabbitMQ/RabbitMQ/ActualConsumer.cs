using RabbitMQ.Client;
using System;
using System.Text;

namespace rabbitMQ
{
    public class ActualConsumer : DefaultBasicConsumer
    {
        public ActualConsumer(IModel model)
            :base(model)
        {

        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {           
            var receivedMessage = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine(receivedMessage);
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);
        }
    }
}
