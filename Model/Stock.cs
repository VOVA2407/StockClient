namespace StockClient.Model;

public record Stock
{
    public int Id { get; set; }
    public string? Symbol { get; set; }
    public string? Name { get; set; }
    public string? Currency { get; set; }
    public string? Exchange { get; set; }
    public string? MicCode { get; set; }
    public string? Country { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}
