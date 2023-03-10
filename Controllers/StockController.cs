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
using StockClient.Services;

namespace StockClient.Controllers;


[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IStockService _stockService;
    private readonly ApiDbContext _context;

    public StockController(ILogger<StockController> logger, IHttpClientFactory clientFactory, IStockService service, ApiDbContext context)
    {
        _logger = logger;
        _httpClientFactory = clientFactory;
        _stockService = service;
        _context = context;
    }

    [HttpGet(Name = "GetStocksFromApi")]
    public async Task<IActionResult> GetStocks()
    {
        _logger.LogInformation("test");

        IEnumerable<Stock> stocks = await _stockService.GetStocksFromExternal();

        if (!stocks.Any())
            return NoContent();

        await _stockService.AddStocks(stocks);
          
        return Ok();
    }

    [HttpGet("GetStocksFromDb")]
    public async Task<IEnumerable<Stock>> GetStocksFromDb()
    {
        var res = await _stockService.GetStocks(CancellationToken.None);
        return res.Take(10);
    }
}
