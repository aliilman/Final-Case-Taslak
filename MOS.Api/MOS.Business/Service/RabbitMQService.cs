using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MOS.Base.DTO;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MOS.Business.Service
{
    public class RabbitMQService : IRabbitMQService
    {
        //https://fish.rmq.cloudamqp.com/#/connections
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration configuration;
        public RabbitMQService(IConfiguration configuration)
        {
            this.configuration=configuration;

            string uri= configuration.GetValue<string>("Queue:Uri");
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void CloseConnection()
        {
            _channel.Close();
            _connection.Close();
        }

        public void SendNotificationQueue(NotificationDTO notification)
        {
            string json = JsonConvert.SerializeObject(notification);

            string queueName =configuration.GetValue<string>("Queue:NotificationQueue");
                        
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

        }
        public void SendPaymentQueue(string PaymentId)
        {
             string queueName =configuration.GetValue<string>("Queue:PaymentQueue");
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(PaymentId);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

        }
    }

}