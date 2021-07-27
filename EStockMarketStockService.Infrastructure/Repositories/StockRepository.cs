using EStockMarketStockService.Domain.Entities;
using EStockMarketStockService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace EStockMarketStockService.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly MongoClient _client;
        private readonly IConfiguration _configuration;
        private readonly string dbName;
        private readonly string collectionName;

        [Obsolete]
        public StockRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var configSection = _configuration.GetSection("CosmosStockDb");
            dbName = configSection.GetSection("DatabaseName").Value;
            collectionName = configSection.GetSection("CollectionName").Value;
            var host = configSection.GetSection("Host").Value;
            var port = Convert.ToInt32(configSection.GetSection("Port").Value);
            var userName = configSection.GetSection("UserName").Value;
            var password = configSection.GetSection("Password").Value;

            MongoClientSettings settings = new MongoClientSettings
            {
                Server = new MongoServerAddress(host, port),
                UseSsl = true,
                RetryWrites = false,
                SslSettings = new SslSettings
                {
                    EnabledSslProtocols = SslProtocols.Tls12
                }
            };

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            _client = new MongoClient(settings);
        }

        public async Task<Stock> AddStockAsync(Stock stock)
        {
            var collection = GetAllStocks();

            await collection.InsertOneAsync(stock);

            return stock;
        }

        public async Task<List<Stock>> GetStocksAsync(string companyCode)
        {
            var collection = await Task.Run(() => GetAllStocks());
            FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyCode", companyCode);
            var stocks = collection?.Find(filter)?.ToList();

            return stocks;
        }

        public async Task<List<Stock>> GetStockPricesAsync(List<string> companyCodes)
        {
            var collection = await Task.Run(() => GetAllStocks());
            List<Stock> stocks = new List<Stock>();

            foreach (var code in companyCodes)
            {
                FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyCode", code);
                var stock = collection?.Find(filter)?.ToList().OrderByDescending(x => x.CreatedDateTime)?.FirstOrDefault();

                if (stock != null)
                    stocks.Add(stock);
            }

            return stocks;
        }

        public async Task DeleteStockbyCompanyCodeAsync(string companyCode)
        {
            var collection = await Task.Run(() => GetAllStocks());
            FilterDefinition<Stock> filter = Builders<Stock>.Filter.Eq("CompanyCode", companyCode);
            collection?.DeleteMany(filter);
        }

        private IMongoCollection<Stock> GetAllStocks()
        {
            var database = _client.GetDatabase(dbName);
            var stockCollection = database.GetCollection<Stock>(collectionName);

            return stockCollection;
        }

    }

}
