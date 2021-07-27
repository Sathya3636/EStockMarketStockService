using EStockMarketStockService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EStockMarketStockService.Application.Queries
{
    public class GetStockPriceQuery : IRequest<List<Stock>>
    {
        public List<string> CompanyCodes { get; set; }
    }
}
