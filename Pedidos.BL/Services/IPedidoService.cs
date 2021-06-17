using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedidos.BL.Models;

namespace Pedidos.BL.Services
{
    public interface IPedidoService : IGenericService<Pedido>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
