
using Microsoft.Build.Framework;

using System.ComponentModel.DataAnnotations;


namespace ProyectoFinal.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
       
        public string Nombre { get; set; }
     
        public string Email { get; set; }
    }
}
