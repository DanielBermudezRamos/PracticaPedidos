using System.ComponentModel.DataAnnotations;

namespace Pedidos.BL.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}