using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Infrastucture;

namespace ConsoleConsumer
{
    public class WeatherService : IHostedService
    {
        private readonly IBus bus;

        public WeatherService(IBus bus)
        {
            this.bus = bus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            bus.Subscribe("getWeather", OnWeatherGetMessage);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void OnWeatherGetMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}