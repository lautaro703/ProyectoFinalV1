using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.DTOs;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ClienteController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("validar/{id}")]
        public async Task<IActionResult> ValidarCliente(int id)
        {
            var cliente = await _appDbContext.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound(); 
            return Ok(cliente); 
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _appDbContext.Clientes
                .Select(c => new { c.Id, c.Nombre })
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            var clientes = await _appDbContext.Clientes.ToListAsync();

            var clientesDtos = clientes.Select(c => new ClienteDto
            {
                Id=c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
            }).ToList();

            return Ok(clientesDtos);
        }

        [HttpPost]
        public async Task<ActionResult<Clientes>> Post(Clientes clientes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _appDbContext.Clientes.Add(clientes);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = clientes.Id }, clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> Get(int id)
        {
            var cliente = await _appDbContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Clientes clientes)
        {
            if (id != clientes.Id)
                return BadRequest();

            _appDbContext.Entry(clientes).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _appDbContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound();

            _appDbContext.Clientes.Remove(cliente);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}