using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LusCore.User
{
    public class UserDto
    {
        [JsonProperty("customerId")]
        public string CustomerId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("hashCode")]
        public string HashCode { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("adress")]
        public string Adreess { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
