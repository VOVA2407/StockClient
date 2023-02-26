using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using StockClient.Database;
using StockClient.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using StockClient.Enums;
using System.Reflection.Metadata.Ecma335;

namespace StockClient.Controllers;


[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public StockController(ILogger<StockController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _httpClientFactory = clientFactory;
    }

    [HttpGet(Name = "GetStocks")]
    public async Task<IActionResult> GetStocks()
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
            Content= encodedContent,
            RequestUri = new Uri("stocks?exchange=NASDAQ&format=json", UriKind.Relative)
        };
       
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            TwelveResponse<Stock>? stocks = await response.Content.ReadFromJsonAsync<TwelveResponse<Stock>>();

            if (!stocks!.Data.Any())
                return NoContent();
              
            using (var context = new ApplicationContext())
            {
                stocks.Data.ToList().ForEach(stock => { context.Stocks.Add(stock); });
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
