namespace ProyectoFinal.DTOs
{
    public class TransaccionDTO
    {
     
        public string CryptoCode { get; set; }
        public string Accion {  get; set; }
        public int ClienteId { get; set; }
        public decimal Cantidad {  get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
