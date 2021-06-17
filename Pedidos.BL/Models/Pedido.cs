using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Pedidos.BL.Models
{
    public partial class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Descripcion { get; set; }

        public virtual ICollection<PedidoProducto> PedidoProducto { get; set; }
    }
}
