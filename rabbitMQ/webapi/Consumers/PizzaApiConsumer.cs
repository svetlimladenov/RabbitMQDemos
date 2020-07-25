using System.Text;
using RabbitMQ.Client;

namespace webapi.Consumers
{
    public class PizzaApiConsumer : DefaultBasicConsumer
    {
        public PizzaApiConsumer(IModel model)
            :base(model)
        {
            
        }


        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, System.ReadOnlyMemory<byte> body)
        {
            var responseMessage = Encoding.UTF8.GetString(body.ToArray());
            System.Console.WriteLine(responseMessage);
        }
    }
}