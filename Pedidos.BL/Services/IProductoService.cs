using Pedidos.BL.Models;
using System.Threading.Tasks;

namespace Pedidos.BL.Services
{
    public interface IProductoService : IGenericService<Producto>
    {
        Task<bool> DeleteCheckOnEntity(int id);
    }
}
