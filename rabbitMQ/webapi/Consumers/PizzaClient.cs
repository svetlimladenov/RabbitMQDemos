using System;
using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Queues;

namespace webapi.Consumers
{
    public class PizzaClient
    {
        private readonly string replyQueueName;
        private readonly IBasicProperties props;
        private readonly IModel channel;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly EventingBasicConsumer consumer;

        public PizzaClient(IModel channel)
        {
            this.channel = channel;            
            this.replyQueueName = channel.QueueDeclare().QueueName;
            
            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (model, ea) => {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    this.respQueue.Add(message);
                }
            };
        }
        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: QueueNames.GetPizzas,
                basicProperties: this.props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take();
        }
    }
}