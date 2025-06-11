using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Criptomoneda
    {
        [Key]
        public string Codigo { get; set; } 

        //public string Nombre { get; set; }

        public List<Transaccion> Transacciones { get; set; }
    }
}
