using System;
using System.Text;
using Infrastucture;
using RabbitMQ.Client;

namespace WebApi.Clients
{
    public class WeatherClient
    {
        private readonly IBus bus;

        public WeatherClient(IBus bus)
        {
            this.bus = bus;
        }
        public void SaveInfo(string username)
        {
            var message = "Hello " + username;
            var weatherExchange = "getWeather";
            this.bus.Publish(weatherExchange, message);
        }
    }
}