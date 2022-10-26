using ApacheKafkaProducerDemo.Models;
using ApacheKafkaProducerDemo.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApacheKafkaProducerDemo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
      private readonly IProducerService _producerService;   

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }   


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            var result = await _producerService.Produce(JsonSerializer.Serialize(orderRequest));

            return Ok(result?"Message sent": "Message couldn't be sent");
        } 
    }
}

