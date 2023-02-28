using Microsoft.EntityFrameworkCore;
using StockClient.Database;
using StockClient.Model;
using System.Net.Http;

namespace StockClient.Services;

public class StockService : IStockService
{
    private readonly ApiDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public StockService(ApiDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Stock[]> GetStocks(CancellationToken cancellationToken)
    {
        return await _context.Stocks.ToArrayAsync();
    }

    public async Task AddStocks(IEnumerable<Stock> stocks)
    {
        stocks.ToList().ForEach(stock => { _context.Stocks.Add(stock); });
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Stock>?> GetStocksFromExternal()
    {
        var client = _httpClientFactory.CreateClient("TwelveApi");

        Dictionary<string, string> parameters = new Dictionary<string, string> {
            { "exchange", "NASDAQ" },
            { "format", "json" }
        };
        FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(parameters);

        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            Content = encodedContent,
            RequestUri = new Uri("stocks?exchange=NASDAQ&format=json", UriKind.Relative)
        };

        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();
        TwelveResponse<Stock>? stocks = await response.Content.ReadFromJsonAsync<TwelveResponse<Stock>>();

        return stocks?.Data;
    }
}
