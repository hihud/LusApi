using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LusCore.Product
{
    public class ProductInfoDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("productid")]
        public string ProductId { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
