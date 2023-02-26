using System.Text.Json.Serialization;

namespace StockClient.Model;

public class TwelveResponse<T>
{
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }
    public string Status { get; set; }
}
