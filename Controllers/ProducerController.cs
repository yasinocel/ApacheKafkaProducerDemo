﻿using ApacheKafkaProducerDemo.Models;
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
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _topic = "testtopic";

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            string message = JsonSerializer.Serialize(orderRequest);
            return Ok(await SendOrderRequest(_topic, message));
        }
        private async Task<bool> SendOrderRequest
        (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
    
