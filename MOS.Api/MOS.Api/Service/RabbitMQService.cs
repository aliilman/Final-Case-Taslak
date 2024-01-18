using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace MOS.Api.Service
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

        public void SendNotificationQueue(string message)
        {
            string queueName =configuration.GetValue<string>("Queue:NotificationQueue");
                        
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
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












    // public class RabbitMQService : IRabbitMQService
    // {
    //     private readonly IConnection _connection;

    //     public RabbitMQService(IConnection connection)
    //     {
    //         _connection = connection;
    //     }

    //     public void SendMessage(string queueName, string message)
    //     {
    //         using (var channel = _connection.CreateModel())
    //         {
    //             // Kuyruğu oluşturun (daha önce oluşturulmamışsa)
    //             channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

    //             // Mesajı JSON formatına çevirip kuyruğa gönderin
    //             var body = Encoding.UTF8.GetBytes(message);
    //             channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    //         }
    //     }


    //     // private readonly IModel _channel;

    //     // public RabbitMQService(IModel channel)
    //     // {
    //     //     _channel = channel;
    //     // }

    //     // public void SendMessage(string queueName, string message)
    //     // {
    //     //     // Kuyruğu oluşturun (daha önce oluşturulmamışsa)
    //     //     _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

    //     //     // Mesajı JSON formatına çevirip kuyruğa gönderin
    //     //     var body = Encoding.UTF8.GetBytes(message);
    //     //     _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    //     // }
    // }


}