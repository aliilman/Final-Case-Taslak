using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOS.Base.DTO;

namespace MOS.Business.Service
{
    public interface IRabbitMQService
    {
        void SendNotificationQueue(NotificationDTO notification);
        void SendPaymentQueue(string PaymentId);
    }
}