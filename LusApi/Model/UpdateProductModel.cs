using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LusApi.Model
{
    public class UpdateProductModel
    {

        [JsonProperty("lusid")]
        public Guid LusId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("collection")]
        public string Collection { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public IFormFile Image { get; set; }
    }
}
