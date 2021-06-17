using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.BL.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<PedidoProducto> PedidoProducto { get; set; }
    }
}
