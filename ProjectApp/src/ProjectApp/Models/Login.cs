using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models
{
    public class Login
    {
        [Key]
        public Guid LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
