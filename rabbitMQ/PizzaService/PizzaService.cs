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

        private PizzaConsumer pizzaConsumer;

        private RpcResponderConsumer rpcResponderConsumer;

        public PizzaService(IModel channel, PizzaConsumer pizzaConsumer, RpcResponderConsumer rpcResponderConsumer)
        {
            this.channel = channel;
            this.pizzaConsumer = pizzaConsumer;
            this.rpcResponderConsumer = rpcResponderConsumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // this.channel.QueueDeclare(QueueNames.CoolQueue, true, false, false, null);
            // this.channel.BasicConsume(QueueNames.CoolQueue, true, this.pizzaConsumer);

            //this.channel.QueueDeclare(QueueNames.RPCQueue, false, false, false, null);
            //this.channel.BasicConsume(QueueNames.RPCQueue, true, this.rpcResponderConsumer);

            this.channel.QueueDeclare(QueueNames.GetPizzas, true, false, false, null);
            this.channel.BasicConsume(QueueNames.GetPizzas, true, this.pizzaConsumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.channel.Close();
            return Task.CompletedTask;
        }
    }
}