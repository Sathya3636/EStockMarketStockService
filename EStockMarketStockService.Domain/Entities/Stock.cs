using Newtonsoft.Json;
using System;

namespace EStockMarketStockService.Domain.Entities
{
    public class Stock
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string CompanyCode { get; set; }

        public double StockPrice { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
