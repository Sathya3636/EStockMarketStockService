using EStockMarketStockService.Models;
using EStockMarketStockService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EStockMarketStockService.Controllers.Test
{
    [TestFixture]
    public class StockControllerTest
    {
        private StockController sut;
        private Mock<IStockService> _stockService;

        [SetUp]
        public void Setup()
        {
            _stockService = new Mock<IStockService>();
            sut = new StockController(_stockService.Object);
        }

        [Test]
        public async Task AddStock_Test()
        {
            _stockService.Setup(x => x.AddStockAsync(It.IsAny<AddStockRequest>())).Returns(Task.FromResult(DefaultValue.Empty));
            var response = await sut.AddStock(new AddStockRequest());

            Assert.Pass();
        }

        [Test]
        public async Task GetStockbyCompanyCode_Test()
        {
            _stockService.Setup(x => x.GetStocksbyCompanyCodeAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(new GetStockResponse()));
            var response = await sut.GetStocksbyCompanyCodeAsync("Comp1", DateTime.Now, DateTime.Now) as ObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(response.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}