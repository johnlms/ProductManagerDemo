using System.Text.Json.Serialization;

namespace ProductManager.Models
{
    public class Product
    {
        [JsonPropertyName("ID")]
        public int? ID { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Price")]
        [JsonNumberHandling(JsonNumberHandling.WriteAsString | JsonNumberHandling.AllowReadingFromString)]
        public decimal? Price { get; set; }
        [JsonPropertyName("Quantity")]
        public int? Quantity { get; set; }
    }
}
