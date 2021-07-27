using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EStockMarketStockService.Models
{
    public class GetStockResponse
    {
        [JsonProperty("stocks")]
        public List<Stock> Stocks { get; set; }

        [JsonProperty("minPrice")]
        public double? MinPrice { get; set; }

        [JsonProperty("maxPrice")]
        public double? MaxPrice { get; set; }

        [JsonProperty("avgPrice")]
        public double? AvgPrice { get; set; }
    }

    public class Stock
    {
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }

        [JsonProperty("stockPrice")]
        public double StockPrice { get; set; }

        [JsonProperty("stockDate")]
        public string StockDate { get; set; }

        [JsonProperty("stockTime")]
        public string StockTime { get; set; }
    }
}
