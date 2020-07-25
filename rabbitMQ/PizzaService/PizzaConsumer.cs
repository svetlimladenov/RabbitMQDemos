using System;
using System.Text;
using RabbitMQ.Client;

namespace PizzaService 
{
    public class PizzaConsumer : DefaultBasicConsumer
    {
        public PizzaConsumer(IModel model)
            :base(model)
        {
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, System.ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());

            var responseMessage = string.Empty;
            if (message == "carbonara")
            {
                System.Console.WriteLine("Creating Carbonara");
                responseMessage = "Creating Carbonara";
            }
            else
            {
                System.Console.WriteLine("We are out of products");
                responseMessage = "We are out of products";
            }

            var replyMessageProperties = this.Model.CreateBasicProperties();
            replyMessageProperties.CorrelationId = properties.CorrelationId;
            System.Console.WriteLine(properties.ReplyTo);
            System.Console.WriteLine(properties.CorrelationId);

            this.Model.BasicPublish("", properties.ReplyTo, replyMessageProperties, Encoding.UTF8.GetBytes(responseMessage));
        }

        public void Publish(string message, string queue)
        {
            var body = Encoding.UTF8.GetBytes(message);
            this.Model.BasicPublish("", queue, null, body);
        }
    }
}