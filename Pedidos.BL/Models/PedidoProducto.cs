using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Pedidos.BL.Models
{
    public partial class PedidoProducto
    {
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public float? Cantidad { get; set; }
        public DateTime? Fecha { get; set; }
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }

    }
}
