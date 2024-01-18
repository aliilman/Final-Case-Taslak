using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOS.Api.Service
{
    public interface IRabbitMQService
    {
        void SendNotificationQueue(string message);
        void SendPaymentQueue(string message);
    }
}