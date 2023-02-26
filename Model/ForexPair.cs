using Microsoft.Identity.Client;

namespace StockClient.Model;

public class ForexPair
{
    public string CurrenceBase { get; set; }
    public string CurrencyGroup { get; set; }
    public string CurrencyQuote { get; set; }
    public string Symbol { get; set; }
}
