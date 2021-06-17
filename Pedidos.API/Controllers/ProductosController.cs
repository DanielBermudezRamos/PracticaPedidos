using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.BL.Data;
using Pedidos.BL.Models;
using Pedidos.BL.Repositories.implements;
using Pedidos.BL.Services.implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pedidos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService productoService = new(new ProductoRepository(PedidosDBContext.Create()));
        // GET: api/<ProductosController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var producto = await productoService.GetAll();
                // PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var producto = await productoService.GetById(id);
                // PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
                if (producto == null)
                    return NotFound();
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<ProductosController>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var productoResultado = await productoService.Insert(producto);
                return Ok(productoResultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (producto.Id != id)
                    return BadRequest();
                var pedidoResultado = await productoService.Update(producto);
                return Ok(pedidoResultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Mala Práctica: " + ex.Message });
            }
        }

        // DELETE api/<ProductosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(int id)
        {
            try
            {
                if (await productoService.GetById(id) == null)
                    return NotFound();
                if (!await productoService.DeleteCheckOnEntity(id))
                    await productoService.Delete(id);
                else
                    throw new Exception("Existe(n) producto(s) relacionados");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
