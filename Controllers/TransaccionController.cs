using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoFinal.DTOs;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransaccionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransaccionDTO>>> Get()
        {
            var transacciones = await _context.Transacciones

                .ToListAsync();

            var transaccioneDtos = transacciones.Select(t => new TransaccionDTO
            {

                CryptoCode = t.CryptoCode,
                Accion = t.Accion,
                ClienteId = t.ClienteId,
                Cantidad = t.Cantidad,
                Monto = t.Monto,
                FechaHora = t.FechaHora

            }).ToList();

            return Ok(transaccioneDtos);
        }

        [HttpGet("transacciones/{clienteId}")]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransaccionesPorCliente(int clienteId)
        {
            return await _context.Transacciones
                .Where(t => t.ClienteId == clienteId)
                .ToListAsync();
        }



        [HttpPost]
        public async Task<ActionResult<Transaccion>> Post([FromBody] Transaccion tranc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Transacciones.Add(tranc);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = tranc.Id }, tranc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<TransaccionDTO>> Get(int id)
        {
            var transaccion = await _context.Transacciones

                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaccion == null)
                return NotFound();

            var dto = new TransaccionDTO
            {
                Id = transaccion.Id,
                CryptoCode = transaccion.CryptoCode,
                Accion = transaccion.Accion,
                ClienteId = transaccion.ClienteId,
                Cantidad = transaccion.Cantidad,
                Monto = transaccion.Monto,
                FechaHora = transaccion.FechaHora
            };
            return Ok(dto);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult>Put(int id, Transaccion transaccion)
        {
            if(id !=transaccion.Id)
                return BadRequest();
            _context.Entry(transaccion).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transaccion = await _context.Transacciones
                .FirstOrDefaultAsync(t => t.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }
            //Eliminar transaccion
            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("EliminarTransaccionPorCliente/{clienteId}")]
        public async Task<IActionResult> DeleteTRA(int clienteId)
        {
            var transacciones = await _context.Transacciones
                .Where(t => t.ClienteId == clienteId)
                .ToListAsync();
            if (transacciones == null || transacciones.Count == 0)
            {
                return NotFound();
            }
            _context.Transacciones.RemoveRange(transacciones);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("obtenerPrecioAsync")]
        public async Task<decimal> obtenerPrecioAsync(string exchange, decimal cantidad, string cripto)
        {
            using (HttpClient cliente = new HttpClient())
            {
                string url = $"https://criptoya.com/api/{exchange}/{cripto}/ars";

                HttpResponseMessage respuesta = await cliente.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = await respuesta.Content.ReadAsStringAsync();
                    var datos = System.Text.Json.JsonDocument.Parse(contenido);

                    if (!datos.RootElement.TryGetProperty("totalAsk", out var precioProp))
                        return 0;

                    decimal precioUnitario = precioProp.GetDecimal();
                    decimal total = precioUnitario * cantidad;

                    return total;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
    

