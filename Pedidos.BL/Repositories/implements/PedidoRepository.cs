using Pedidos.BL.Models;
using Pedidos.BL.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.BL.Repositories.implements
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly PedidosDBContext pedidosContext;
        public PedidoRepository(PedidosDBContext pedidosDBContext) : base(pedidosDBContext)
        {

        }

        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            var flag = await pedidosContext.PedidoProductos.AnyAsync(x => x.PedidoId == id);
            return flag;
        }
    }
}
