using System.ComponentModel.DataAnnotations;

namespace Pedidos.BL.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}
