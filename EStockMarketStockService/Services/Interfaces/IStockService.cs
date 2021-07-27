using EStockMarketStockService.Domain.Entities;
using EStockMarketStockService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EStockMarketStockService.Services.Interfaces
{
    public interface IStockService
    {
        Task AddStockAsync(AddStockRequest request);

        Task<List<Domain.Entities.Stock>> GetLatestPricesStockAsync(GetStockPriceRequest request);

        Task<GetStockResponse> GetStocksbyCompanyCodeAsync(string companyCode, DateTime startDate, DateTime endDate);
    }
}
