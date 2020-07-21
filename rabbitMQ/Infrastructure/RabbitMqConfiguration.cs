using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMQ(this IServiceCollection services, string hostName = "localhost")
        {
            var factory = new ConnectionFactory();
            factory.HostName = hostName;

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            services.AddSingleton<IModel>(channel);
        }
    }
}