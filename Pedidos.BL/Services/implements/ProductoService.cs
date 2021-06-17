using Pedidos.BL.Models;
using Pedidos.BL.Repositories;
using System.Threading.Tasks;

namespace Pedidos.BL.Services.implements
{
    public class ProductoService : GenericService<Producto>
    {
        private readonly IProductoRepository producto;
        public ProductoService(IProductoRepository productoRepository) : base(productoRepository)
        {
            producto = productoRepository;
        }
      
        public async Task<bool> DeleteCheckOnEntity(int id)
        {
            return await producto.DeleteCheckOnEntity(id);
        }
    }
}
