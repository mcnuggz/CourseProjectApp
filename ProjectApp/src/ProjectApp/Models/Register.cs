using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models
{
    public class Register
    {
        [Key]
        public Guid RegisterId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
