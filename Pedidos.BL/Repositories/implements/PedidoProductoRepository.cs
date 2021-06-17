using Pedidos.BL.Models;
using Pedidos.BL.Data;

namespace Pedidos.BL.Repositories.implements
{
    public class PedidoProductoRepository : GenericRepository<PedidoProducto>, IPedidoProductoRepository
    {
        public PedidoProductoRepository(PedidosDBContext pedidosDBContext) : base(pedidosDBContext)
        {

        }
    }
}
