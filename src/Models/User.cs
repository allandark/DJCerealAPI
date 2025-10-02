using System.ComponentModel.DataAnnotations;

namespace CerealAPI.src.Models
{
    /// <summary>
    /// User model for database
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
