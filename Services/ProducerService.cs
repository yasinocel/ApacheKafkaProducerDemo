using Confluent.Kafka;
using System.Diagnostics;
using System.Net;

namespace ApacheKafkaProducerDemo.Services
{
    public class ProducerService : IProducerService
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _topic = "testtopic2";

        public async Task<bool> Produce(string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(_topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error occured: {ex.Message}");
            }

            return false;
        }
    }
}
