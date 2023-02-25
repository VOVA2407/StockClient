using Microsoft.AspNetCore.Mvc;
using StockClient.Database;
using StockClient.Model;

namespace StockClient.Controllers;


[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private ILogger<StockController> _logger;

    public StockController(ILogger<StockController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetStocks")]
    public async void GetStocks()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://twelve-data1.p.rapidapi.com/stocks?exchange=NASDAQ&format=json"),
            Headers =
    {
        { "X-RapidAPI-Key", "be0aa279f7msh3b546c10c7a956ap1e30f0jsn733747cd1cd9" },
        { "X-RapidAPI-Host", "twelve-data1.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            _logger.LogInformation("test");
            Stocks stocks = await response.Content.ReadFromJsonAsync<Stocks>();

            using (var context = new ApplicationContext())
            {
                foreach(Stock stock in stocks.Stock)
                    context.Stocks.Add(stock);
             
                context.SaveChanges();
            }

        }
    }
}
