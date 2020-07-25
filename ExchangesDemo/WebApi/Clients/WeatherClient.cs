using System;
using System.Text;
using RabbitMQ.Client;

namespace WebApi.Clients
{
    public class WeatherClient
    {
        private readonly IModel channel;
        private readonly IConnection connection;
        public WeatherClient()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            this.connection = factory.CreateConnection();
            this.channel = this.connection.CreateModel();
        }

        public void Stop()
        {
            this.channel.Close();
            this.connection.Close();
        }

        public void SaveInfo(string username)
        {
            var message = Encoding.UTF8.GetBytes(username);
            var weatherExchange = "getWeather";
            this.channel.ExchangeDeclare(weatherExchange, "fanout");
            this.channel.BasicPublish(weatherExchange, "", null, message);
        }
    }
}