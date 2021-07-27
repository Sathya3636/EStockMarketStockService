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
    public class GetStockQueryHandler : IRequestHandler<GetStockQuery, List<Stock>>
    {
        private readonly IStockRepository _stockRepository;

        public GetStockQueryHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<Stock>> Handle(GetStockQuery request, CancellationToken cancellationToken)
        {
            return await _stockRepository.GetStocksAsync(request.CompanyCode);
        }
    }
}
