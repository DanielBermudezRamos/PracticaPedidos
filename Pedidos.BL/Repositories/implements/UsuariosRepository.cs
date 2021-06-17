using Pedidos.BL.Models;
using Pedidos.BL.Data;

namespace Pedidos.BL.Repositories.implements
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(PedidosDBContext pedidosDBContext) : base(pedidosDBContext)
        {

        }
    }
}
