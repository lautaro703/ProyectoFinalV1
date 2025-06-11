using System.Diagnostics;
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

                CryptoCode = t.cryptoCode,
                Accion = t.accion,
                ClienteId = t.Id,
                Cantidad=t.Cantidad,
                Monto = t.monto,
                FechaHora = t.fechaHora

            }).ToList();

            return Ok(transaccioneDtos);
        }


        [HttpPost]
        public async Task<ActionResult<Transaccion>> Post([FromBody] Transaccion tranc)
        {
            try
            {
                _context.Transacciones.Add(tranc);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { Id = tranc.Id }, tranc);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                CryptoCode = transaccion.cryptoCode,
                Accion = transaccion.accion,
                ClienteId = transaccion.Id,
                Cantidad=transaccion.Cantidad,
                Monto = transaccion.monto,
                FechaHora = transaccion.fechaHora
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
    

