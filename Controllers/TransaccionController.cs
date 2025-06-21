using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Cantidad=t.Cantidad,
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

        /*[HttpGet("precio")]
        public async Task<IActionResult> ObtenerPrecio(string codigo, decimal cantidad)
        {
            using var httpClient = new HttpClient();
            string url = $"https://criptoya.com/api/bybit/{codigo}/ARS/{cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

            try
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return BadRequest("Error al obtener precio desde CriptoYa");

                var contenido = await response.Content.ReadAsStringAsync();
                return Content(contenido, "application/json");
            }
            catch
            {
                return StatusCode(500, "Error interno");
            }
        }*/

        [HttpGet("precio")]
        public async Task<IActionResult> ObtenerMonto(string codigo, decimal cantidad)
        {
            if (string.IsNullOrEmpty(codigo) || cantidad <= 0)
                return BadRequest("Código o cantidad inválida.");

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://criptoya.com/api/{codigo}/ars");

                if (!response.IsSuccessStatusCode)
                    return StatusCode(502, "Error al consultar CriptoYa.");

                var data = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(data);

                if (!json.RootElement.TryGetProperty("totalbid", out var precioJson))
                    return BadRequest("Respuesta inválida de CriptoYa.");

                decimal precio = precioJson.GetDecimal();
                decimal monto = precio * cantidad;

                return Ok(monto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
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
              
                .FirstOrDefaultAsync(t=>t.Id==id);

            if (transaccion == null)
                return NotFound();

            var dto = new TransaccionDTO
            {
                CryptoCode = transaccion.CryptoCode,
                Accion = transaccion.Accion,
                ClienteId = transaccion.Id,
                Cantidad=transaccion.Cantidad,
                Monto = transaccion.Monto,
                FechaHora = transaccion.FechaHora
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>Put(int id, Transaccion transaccion)
        {
            if(id !=transaccion.Id)
                return BadRequest();
            _context.Entry(transaccion).State= EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

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

    }
}
    

