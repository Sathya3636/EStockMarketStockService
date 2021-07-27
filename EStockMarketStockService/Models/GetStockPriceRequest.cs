using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EStockMarketStockService.Models
{
    public class GetStockPriceRequest
    {
        [Required]
        [JsonProperty("companyCodes")]
        public List<string> CompanyCodes { get; set; }
    }
}
