using EStockMarketStockService.Domain.Entities;
using EStockMarketStockService.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EStockMarketStockService.Application.Commands
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, Stock>
    {
        private readonly IStockRepository _stockRepository;

        public AddStockCommandHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Stock> Handle(AddStockCommand command, CancellationToken cancellationToken)
        {
            return await _stockRepository.AddStockAsync(command.Stock);
        }
    }
}
