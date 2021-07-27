using EStockMarketStockService.Domain.Entities;
using EStockMarketStockService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EStockMarketStockService.Application.Queries
{
    public class GetStockPriceQueryHandler : IRequestHandler<GetStockPriceQuery, List<Stock>>
    {
        private readonly IStockRepository _stockRepository;

        public GetStockPriceQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<Stock>> Handle(GetStockPriceQuery request, CancellationToken cancellationToken)
        {
            return await _stockRepository.GetStockPricesAsync(request.CompanyCodes);
        }
    }
}
