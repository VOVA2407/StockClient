using System.Text.Json.Serialization;

namespace StockClient.Model;

public class Stocks
{
    [JsonPropertyName("data")]
    public Stock[] Stock {get;set;}
    public string Status { get; set; }

}
