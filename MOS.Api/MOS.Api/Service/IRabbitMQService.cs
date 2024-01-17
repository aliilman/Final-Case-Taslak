using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOS.Api.Service
{
    public interface IRabbitMQService
    {
        void SendMessage(string queueName, string message);
    }
}