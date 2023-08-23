using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaspetWEBUI.Core.DTO
{
    using System.Text.Json.Serialization;

    public class LoginDTO
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        [JsonIgnore] 
        public string Password { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public string? Email { get; set; }

        public string Address { get; set; }

        public string UserRole { get; set; }

    }
}
