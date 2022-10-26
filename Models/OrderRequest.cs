namespace ApacheKafkaProducerDemo.Models
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }        
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
