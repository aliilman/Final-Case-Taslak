using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOS.Api.Service;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMQConsumerController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IRabbitMQConsumerService rabbitMQConsumerService;

        public RabbitMQConsumerController(IRabbitMQService rabbitMQService, IRabbitMQConsumerService rabbitMQConsumerService)
        {
            _rabbitMQService = rabbitMQService;
            this.rabbitMQConsumerService = rabbitMQConsumerService;
        }

        [HttpGet("SendToRabbitMQ/{mesaj}")]
        public IActionResult sendMessages( string mesaj)
        {
            _rabbitMQService.SendMessage("myQueue", mesaj);

            return Ok("Listening for messages from RabbitMQ.");
        }
        [HttpGet("GetFromRabbitMQ")]
        public IActionResult GetMessages()
        {

            return Ok(rabbitMQConsumerService.ReceiveMessage("myQueue"));
        }
    }
}