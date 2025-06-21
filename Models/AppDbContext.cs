using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
    }
}
