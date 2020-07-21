using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ;
using RabbitMQ.Client;
using Queues;

namespace PizzaService
{
    public class PizzaService : IHostedService
    {
        // Injected
        private IModel channel;   
        public PizzaService(IModel channel, PizzaConsumer pizzaConsumer)
        {
            this.channel = channel;
            this.PizzaConsumer = pizzaConsumer;
        }

        public PizzaConsumer PizzaConsumer { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.channel.QueueDeclare(QueueNames.CoolQueue, true, false, false, null);
            this.channel.BasicConsume(QueueNames.CoolQueue, true, this.PizzaConsumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.channel.Close();
            return Task.CompletedTask;
        }
    }
}