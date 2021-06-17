using Pedidos.BL.Models;
using Pedidos.BL.Repositories;
using System.Threading.Tasks;

namespace Pedidos.BL.Services.implements
{
    public class PedidoService : GenericService<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository pedido;
        public PedidoService(IPedidoRepository pedidoRepository) : base(pedidoRepository)
        {
            pedido = pedidoRepository;       
        }
        public async Task<bool> DeleteCheckOnEntity(int id)
        {            
            return await pedido.DeleteCheckOnEntity(id);
        }
    }
}
