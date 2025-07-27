using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        [NotMapped]
        public string Password { get; set; }  

        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
