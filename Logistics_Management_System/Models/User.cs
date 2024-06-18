using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logistics_Management_System.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponse
    {

        public string? Role { get; set; }
        public string? Token { get; set; }
        public int UserId { get; set; }

        public string? Message { get; set; }
    }
}