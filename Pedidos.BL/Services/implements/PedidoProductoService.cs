using Pedidos.BL.Models;
using Pedidos.BL.Repositories;

namespace Pedidos.BL.Services.implements
{
    public class PedidoProductoService : GenericService<PedidoProducto>
    {
        public PedidoProductoService(IPedidoProductoRepository pedidoProductoRepository) : base(pedidoProductoRepository)
        {

        }
    }
}
