using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MOS.Api.Service
{
    public class RabbitMQConsumerService : IRabbitMQConsumerService
    {
        public RabbitMQConsumerService()
        {

        }

        public string ReceiveMessage(string queueName)
        {
            string mesaj = null;
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ocdvcuen:0Qh_eUzUCDoG-iHudqT8P4NGOAf6P2vf@fish.rmq.cloudamqp.com/ocdvcuen");

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queueName, false, consumer);
                consumer.Received += (sender, ex) =>
                {
                    //e.Body : Kuyruktaki mesajı verir.
                    mesaj = Encoding.UTF8.GetString(ex.Body.ToArray());
                    channel.BasicAck(ex.DeliveryTag, false);
                };

            }
            return mesaj;
        }


        // public void ReceiveMessage(string queueName)
        // {

        //     // Kuyruğu oluşturun (daha önce oluşturulmamışsa)
        //     _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        //     // Mesajları işleyecek bir event handler oluşturun
        //     var consumer = new EventingBasicConsumer(_channel);
        //     consumer.Received += (model, ea) =>
        //     {
        //         var body = ea.Body.ToArray();
        //         var message = Encoding.UTF8.GetString(body);

        //         // Mesajı işleyin (bu örnekte sadece consola yazdık)
        //         Console.WriteLine($"Received message: {message}");

        //         // Mesajın işlendiğini RabbitMQ'ya bildirin
        //         _channel.BasicAck(ea.DeliveryTag, false);
        //     };

        //     // Kuyruğa abone olun
        //     _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        // }
    }
}