using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LusApi.Model
{
    public class LoginUserModel
    {
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Adreess { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Token { get; set; }
    }
}
