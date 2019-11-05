using System;
using System.Collections.Generic;
using System.Text;

namespace LusCore.User
{
    public class UserModel
    {
        public string Username { get; set; }
        public string HashCode { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
    }
}
