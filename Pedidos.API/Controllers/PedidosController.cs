using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.BL.Data;
// using Pedidos.BL.DTOs;
using Pedidos.BL.Models;
using Pedidos.BL.Repositories.implements;
using Pedidos.BL.Services.implements;
using System;
using System.Threading.Tasks;

namespace Pedidos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        //private readonly IMapper _mapper;
        private readonly PedidoService pedidoService = new(new PedidoRepository(PedidosDBContext.Create()));

        //public PedidosController(IMapper mapper)
        //{
            //_mapper = mapper;
        //}
        /// <summary>
        /// Obtiene los Objetos de Pedidos
        /// </summary>
        /// <returns>listado completo de los Pedidos</returns>
        /// <response code="200">Ok. Devuelve la lista completa de Pedidos</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pedido = await pedidoService.GetAll();
                // PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedido = await pedidoService.GetById(id);
                // PedidoDTO pedidoDTO = _mapper.Map<PedidoDTO>(pedido);
                if (pedido == null)
                    return NotFound();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var pedidoResultado = await pedidoService.Insert(pedido);
                return Ok(pedidoResultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if(pedido.Id != id)
                    return BadRequest();
                /*var flag = await pedidoService.GetById(id); // <--- produce un error
                if ( flag == null)
                    return NotFound();
                flag = null;*/
                var pedidoResultado = await pedidoService.Update(pedido);
                return Ok(pedidoResultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Mala Práctica: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(int id)
        {
            try
            {
                if (await pedidoService.GetById(id) == null)
                    return NotFound();
                if (!await pedidoService.DeleteCheckOnEntity(id))
                    await pedidoService.Delete(id);
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
