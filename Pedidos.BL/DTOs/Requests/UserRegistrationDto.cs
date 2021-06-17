using System.ComponentModel.DataAnnotations;

namespace Pedidos.BL.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}