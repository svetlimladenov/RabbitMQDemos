using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Queues;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbitMQ
{
    public class Program
    {
        public static void Main()
        {
            var rabbitMQhostname = "localhost";
            var factory = new ConnectionFactory();
            factory.HostName = rabbitMQhostname;
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            var myQueue = QueueNames.CoolQueue;

            channel.QueueDeclare(myQueue, true, false, false, null);

            Console.WriteLine("Type in messages");
            while (true)
            {
                var message = CreateMessage();
                channel.BasicPublish("", myQueue, null, message);
            }

            channel.Close();
            connection.Close();
        }

        private static byte[] CreateMessage()
        {
            var content = Console.ReadLine();
            var message = new HelloMessage()
            {
                Id = 1,
                MessageContent = content
            };

            var jsonMessage = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(jsonMessage);
        }
    }
}
