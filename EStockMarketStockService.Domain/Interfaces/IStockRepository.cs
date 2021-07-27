using EStockMarketStockService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStockMarketStockService.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> AddStockAsync(Stock stock);

        Task<List<Stock>> GetStocksAsync(string companyCode);

        Task<List<Stock>> GetStockPricesAsync(List<string> companyCodes);

        Task DeleteStockbyCompanyCodeAsync(string companyCode);
    }
}
