using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.BL.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo es Requerido")] 
        public string Cliente { get; set; }
        [Required(ErrorMessage = "El Campo es Requerido")] 
        public string Descripcion { get; set; }
    }
}
