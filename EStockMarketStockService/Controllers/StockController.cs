using EStockMarketStockService.Models;
using EStockMarketStockService.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EStockMarketStockService.Controllers
{
    [Route("api/v1.0/market/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        {
            try
            {
                await _stockService.AddStockAsync(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }

        [Route("get/latestprice")]
        [HttpPost]
        public async Task<IActionResult> GetLatestStockPriceforCompanies(GetStockPriceRequest request)
        {
            try
            {
                var stocks = await _stockService.GetLatestPricesStockAsync(request);

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }

        [Route("get/{companyCode}/{startDate}/{endDate}")]
        [HttpGet]
        public async Task<IActionResult> GetStocksbyCompanyCodeAsync(string companyCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                var stockResponse = await _stockService.GetStocksbyCompanyCodeAsync(companyCode, startDate, endDate);

                return Ok(stockResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }
    }
}
