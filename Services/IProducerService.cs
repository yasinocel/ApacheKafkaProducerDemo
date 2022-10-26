namespace ApacheKafkaProducerDemo.Services
{
    public interface IProducerService
    {
        Task<bool> Produce(string message);
    }
}
