using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Infrastucture
{
    public abstract class BaseService : IHostedService
    {
        private readonly IBus bus;

        public BaseService(IBus bus)
        {
            this.bus = bus;
        }

        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
        
        protected void Subscribe(string exchange, Action<string> action)
        {
            bus.Subscribe(exchange, action);
        }

        protected void Subscribe<T>(Action<T> action)
        {
            bus.Subscribe<T>(action);
        }
    }
}