using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedidos.BL.Models;

namespace Pedidos.BL.Repositories
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
