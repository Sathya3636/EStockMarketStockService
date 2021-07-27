using EStockMarketStockService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EStockMarketStockService.Application.Queries
{
    public class GetStockQuery : IRequest<List<Stock>>
    {
        public string CompanyCode { get; set; }
    }
}
