using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastucture;
using Contracts.Messages;

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
            //Subscribe("getWeather", OnWeatherGetMessage);
            Subscribe<GetWeatherMessage>(OnWeatherGetMessage);
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

        public void OnWeatherGetMessage(GetWeatherMessage message)
        {
            Console.WriteLine(message.Username);
        }
    }
}