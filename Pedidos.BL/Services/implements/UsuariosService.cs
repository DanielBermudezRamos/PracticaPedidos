using Pedidos.BL.Models;
using Pedidos.BL.Repositories;

namespace Pedidos.BL.Services.implements
{
    public class UsuariosService : GenericService<Usuarios>
    {
        public UsuariosService(IUsuariosRepository usuariosRepository) : base(usuariosRepository)
        {

        }
    }
}
