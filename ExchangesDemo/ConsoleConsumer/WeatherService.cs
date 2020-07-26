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
    public class WeatherService : BaseService
    {
        public WeatherService(IBus bus)
            :base(bus)
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Subscribe("getWeather", OnWeatherGetMessage);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void OnWeatherGetMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}