using System;
using System.Text;
using System.Threading.Tasks;
using Infrastucture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsoleConsumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            await builder.RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                
                services.AddSingleton<IBus, BasicBus>();
                services.AddSingleton<IHostedService, WeatherService>();
            });
    }
}
