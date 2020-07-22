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

            var rpcQueue = QueueNames.RPCQueue;
            channel.QueueDeclare(rpcQueue, false, false, false, null);
            var rcpCallerConsumer = new RpcCallerConsumer(channel);
            //temporart, private autodelete reply queue
            var replyQueue = channel.QueueDeclare().QueueName;
            channel.BasicConsume(replyQueue, true, rcpCallerConsumer);




            //Tuka suzdavame message koito e RPC - 
            // setva mu se
            // 1. correlation Id
            // 2.Reply To - ime na queue do koeto da replyne

            var properties = channel.CreateBasicProperties();
            properties.CorrelationId = "moito correlation id guid";
            properties.ReplyTo = replyQueue;
            var nekuvMessage = Console.ReadLine();
            var nekvoBody = Encoding.UTF8.GetBytes(nekuvMessage);
            // publishvame rpc messagea
            channel.BasicPublish("", rpcQueue, properties, nekvoBody);

            Console.ReadLine();
            channel.Close();
            connection.Close();
        }
    }
}
