using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LusCore.Product
{
    public class ProductDto
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
        [JsonProperty("material")]
        public string Material { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
