namespace LaBancaUCB.Core.DTOs
{
    public class CuentaQueryFilterDto
    {
        public string? Estado { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
