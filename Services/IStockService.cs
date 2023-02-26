using StockClient.Model;

namespace StockClient.Services;

public interface IStockService
{
    Task<Stock[]> GetStocks(CancellationToken cancellationToken);
    Task AddStocks(IEnumerable<Stock> stocks);
}
