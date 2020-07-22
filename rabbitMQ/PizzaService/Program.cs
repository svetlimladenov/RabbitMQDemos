using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Serilog;

namespace PizzaService
{
    public class Program
    {
        public static async Task Main()
        {
            var builder = new HostBuilder()
                        .ConfigureServices((hostContext, services) => 
                        {
                            services.AddRabbitMQ();
                            services.AddSingleton<PizzaConsumer>();
                            services.AddSingleton<RpcResponderConsumer>();
                            services.AddHostedService<PizzaService>();
                        })
                        .ConfigureLogging((hostingContext, logging) => logging.AddSerilog());

            await builder.RunConsoleAsync();
        }
    }
}