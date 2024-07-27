using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashCollectionAPI.Data;

namespace TrashCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitTestController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public RabbitTestController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost("createQueue")]
        public IActionResult CreateQueue([FromQuery] string queueName)
        {
            _rabbitMQService.CreateQueue(queueName);
            return Ok();
        }

        [HttpPost("sendMessage")]
        public IActionResult SendMessage([FromBody] SendMessageRequest request)
        {
            _rabbitMQService.SendMessage(request.QueueName, request.Message);
            return Ok();
        }
    }

    public class SendMessageRequest
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
    }
}

