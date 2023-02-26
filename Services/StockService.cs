using Microsoft.EntityFrameworkCore;
using StockClient.Database;
using StockClient.Model;

namespace StockClient.Services;

public class StockService : IStockService
{
    private readonly ApiDbContext _context;

    public StockService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Stock[]> GetStocks(CancellationToken cancellationToken)
    {
        return await _context.Stocks.ToArrayAsync();
    }
}
