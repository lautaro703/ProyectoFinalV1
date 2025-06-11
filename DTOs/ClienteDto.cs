using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.DTOs
{
    public class ClienteDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set;}

        [Required]
        public string? Email { get; set; }
    }
}
