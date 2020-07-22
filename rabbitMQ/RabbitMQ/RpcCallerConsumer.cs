using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace rabbitMQ
{
    public class RpcCallerConsumer : DefaultBasicConsumer
    {
        public RpcCallerConsumer(IModel channel)
            :base(channel)
        {

        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var messageIdentifier = properties.CorrelationId;
            //nesho se pravi s tva correlation id;
            AddAction("wTF", body.ToArray());
            
        }

        public void AddAction(string action, byte[] body)
        {
            Console.WriteLine(Encoding.UTF8.GetString(body));
        }
    }
}
