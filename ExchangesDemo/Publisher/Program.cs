using System;
using System.Text;
using RabbitMQ.Client;

namespace Publisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            var myExchange = "myExchange";
            channel.ExchangeDeclare(myExchange, "fanout");
            channel.BasicPublish(myExchange, "", null, Encoding.UTF8.GetBytes("supp"));
        }
    }
}
