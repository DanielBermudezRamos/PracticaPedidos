using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedidos.BL.Models;
using Pedidos.BL.Repositories.implements;

namespace Pedidos.BL.Repositories
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
