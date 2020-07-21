using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondUser
{
    public class AnotherConsumer : DefaultBasicConsumer
    {
        public AnotherConsumer(IModel model)
            :base(model)
        {

        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var bodyArray = body.ToArray();
            var message = Encoding.UTF8.GetString(bodyArray);
            Console.WriteLine(message);
        }
    }
}
