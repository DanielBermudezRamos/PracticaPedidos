using AutoMapper;
using Pedidos.BL.Models;
using Pedidos.BL.DTOs.Requests;

namespace Pedidos.BL.DTOs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Pedido, PedidoDTO>(); // GET
            // CreateMap<PedidoDTO, Pedido>();  // POST-PUT

            // CreateMap<Producto, ProductoDTO>(); // GET
            // CreateMap<ProductoDTO, Producto>();  // POST-PUT

            // CreateMap<Pedido, PedidoDTO>(); // GET
            // CreateMap<PedidoDTO, Pedido>();  // POST-PUT

        }
        public static MapperConfiguration MapperConfiguration() 
        {
            return new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Pedido, PedidoDTO>(); // GET
                    cfg.CreateMap<PedidoDTO, Pedido>();  // POST-PUT
                });
        }
    }
}
