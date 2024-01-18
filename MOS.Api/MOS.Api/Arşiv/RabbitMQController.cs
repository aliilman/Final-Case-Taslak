// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using MOS.Api.Service;

// namespace MOS.Api.Arşiv
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [NonController] // 
//     public class RabbitMQController : ControllerBase
//     {
//         private readonly IRabbitMQService _rabbitMQService;
//         private readonly IRabbitMQConsumerService rabbitMQConsumerService;

//         public RabbitMQController(IRabbitMQService rabbitMQService, IRabbitMQConsumerService rabbitMQConsumerService)
//         {
//             _rabbitMQService = rabbitMQService;
//             this.rabbitMQConsumerService = rabbitMQConsumerService;
//         }

//         [HttpGet("SendToRabbitMQ")]
//         public IActionResult sendMessages( [FromQuery] string? PaymentId,[FromQuery] string? mesaj)
//         {
//             _rabbitMQService.SendPaymentQueue( PaymentId );
//             _rabbitMQService.SendNotificationQueue( mesaj);

//             return Ok("Listening for messages from RabbitMQ.");
//         }
//         [HttpGet("GetFromRabbitMQ")]
//         public IActionResult GetMessages()
//         {
//             return Ok(rabbitMQConsumerService.ReceiveNotificationQueue()+rabbitMQConsumerService.ReceivePaymentQueue());
//         }
//     }
// }