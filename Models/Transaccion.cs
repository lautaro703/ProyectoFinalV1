﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoFinal.Models
{

    public class Transaccion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de la criptomoneda es obligatorio.")]
        [ForeignKey("Criptomoneda")]
        public string cryptoCode { get; set; }

        //public Criptomoneda Criptomoneda { get; set; }

      
       // public Clientes Clientes { get; set; }

        

        public string accion { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La cantidad de criptomonedas es obligatoria.")]
        [Range(0.00000001, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El monto en ARS es obligatorio.")]
        [Range(0.00000001, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal monto { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        public DateTime fechaHora { get; set; }
    }
}

