using Microsoft.EntityFrameworkCore;
using Pedidos.BL.Data;
using Pedidos.BL.Models;
using System.Threading.Tasks;

namespace Pedidos.BL.Repositories.implements
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly PedidosDBContext pedidosContext;
        public ProductoRepository(PedidosDBContext pedidoDBContext) : base(pedidoDBContext)
        {
            pedidosContext = pedidoDBContext;
        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            return await pedidosContext.PedidoProductos.AnyAsync(x => x.PedidoId == id);
        }
    }
}
