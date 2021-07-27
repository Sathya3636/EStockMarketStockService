using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EStockMarketStockService.Models
{
    public class AddStockRequest
    {
        [Required]
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }

        [Required]
        [JsonProperty("stockPrice")]
        public double StockPrice { get; set; }
    }
}
