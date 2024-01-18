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
        private readonly IConfiguration configuration;
        public RabbitMQConsumerService(IConfiguration configuration)
        {
            this.configuration=configuration;
        }

        public string ReceiveNotificationQueue()
        {       
            string queueName =configuration.GetValue<string>("Queue:NotificationQueue");
            string uri= configuration.GetValue<string>("Queue:Uri");
            string mesaj = null;
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);

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
        public string ReceivePaymentQueue()
        {
            string queueName =configuration.GetValue<string>("Queue:PaymentQueue");
            string uri= configuration.GetValue<string>("Queue:Uri");
            string mesaj = null;
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);

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

    }
}