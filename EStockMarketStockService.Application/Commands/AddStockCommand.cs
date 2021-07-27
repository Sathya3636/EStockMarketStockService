using EStockMarketStockService.Domain.Entities;
using MediatR;

namespace EStockMarketStockService.Application.Commands
{
    public class AddStockCommand : IRequest<Stock>
    {
        public Stock Stock { get; set; }
    }
}
