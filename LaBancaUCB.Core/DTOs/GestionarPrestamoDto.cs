namespace LaBancaUCB.Core.DTOs
{
    public class GestionarPrestamoDto
    {
        public decimal MontoAprobado { get; set; }
        public decimal TasaInteres { get; set; }
        public string Estado { get; set; } = null!; // "aprobado", "rechazado", "pagado"
    }
}
