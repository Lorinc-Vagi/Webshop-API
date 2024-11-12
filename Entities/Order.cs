using System.Text.Json.Serialization;

namespace Webshop_API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public virtual List<Product>? Products { get; set; } = new List<Product>();
    }
}
