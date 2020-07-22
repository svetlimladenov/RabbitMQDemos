using System.Text;
using RabbitMQ.Client;

namespace PizzaService
{
    public class RpcResponderConsumer : DefaultBasicConsumer
    {
        public RpcResponderConsumer(IModel channel)
            :base(channel)
        {
            
        }


        //hvashtame rpc message-a
        //imame mu correlation Id-to
        //imame i ReplyTo - imeto na Queue-to na koeto trqbva da replyneme
        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, System.ReadOnlyMemory<byte> body)
        {
            //Recived the message called from the rpc caller and print it here
            System.Console.WriteLine(Encoding.UTF8.GetString(body.ToArray()));


            // 1.Perform the action required in the RPC
            var replyMessage = "Replying to the message aahah";

            // 2.Prepare the reply message
            var replyMessageBody = Encoding.UTF8.GetBytes(replyMessage);

            // 3.Set the correlation Id in the reply properties (za da se znae tochno na koi da se prati message-a)      
            var replyMessageProperties = this.Model.CreateBasicProperties();
            replyMessageProperties.CorrelationId = properties.CorrelationId;
            
            // 4. Send the response to the queue - (ReplyTo )
            this.Model.BasicPublish("", properties.ReplyTo, true, replyMessageProperties, replyMessageBody);
            this.Model.BasicAck(deliveryTag, false);
        }
    }
}