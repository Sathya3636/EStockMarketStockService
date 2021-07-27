using EStockMarketStockService.Models;
using EStockMarketStockService.Services.Interfaces;
using EStockMarketStockService.Application.Commands;
using MediatR;
using System.Threading.Tasks;
using EStockMarketStockService.Application.Queries;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EStockMarketStockService.Services
{
    public class StockService : IStockService
    {
        private readonly IMediator _mediator;

        public StockService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddStockAsync(AddStockRequest request)
        {
            var command = new AddStockCommand
            {
                Stock = new Domain.Entities.Stock
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyCode = request.CompanyCode,
                    StockPrice = request.StockPrice,
                    CreatedDateTime = DateTime.Now
                }
            };

            await _mediator.Send(command);
        }

        public async Task<List<Domain.Entities.Stock>> GetLatestPricesStockAsync(GetStockPriceRequest request)
        {
            var query = new GetStockPriceQuery
            {
                CompanyCodes = request.CompanyCodes
            };

            return await _mediator.Send(query);
        }

        public async Task<GetStockResponse> GetStocksbyCompanyCodeAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            GetStockResponse stockResponse = new GetStockResponse
            {
                Stocks = new List<Models.Stock>()
            };
            var query = new GetStockQuery
            {
                CompanyCode = companyCode
            };

            var stocks = await _mediator.Send(query);

            stocks = stocks?.Where(x => x.CreatedDateTime.Date >= startDate.Date && x.CreatedDateTime.Date <= endDate.Date)?.ToList();
            stockResponse.MinPrice = stocks?.Min(x => x?.StockPrice);
            stockResponse.MaxPrice = stocks?.Max(x => x?.StockPrice);
            stockResponse.AvgPrice = stocks?.Average(x => x?.StockPrice);

            foreach (var stock in stocks)
            {
                stockResponse.Stocks.Add(
                    new Models.Stock
                    {
                        CompanyCode = stock.CompanyCode,
                        StockPrice = stock.StockPrice,
                        StockDate = stock.CreatedDateTime.ToShortDateString(),
                        StockTime = stock.CreatedDateTime.ToShortTimeString()
                    });
            }

            return stockResponse;
        }
    }
}
