using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Data;

public partial class LaBancaUCBContext : DbContext
{
    public LaBancaUCBContext(DbContextOptions<LaBancaUCBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Sesione> Sesiones { get; set; }
    public virtual DbSet<Cuenta> Cuentas { get; set; }
    public virtual DbSet<Beneficiario> Beneficiarios { get; set; }
    public virtual DbSet<Transaccion> Transacciones { get; set; }
    public virtual DbSet<Seguro> Seguros { get; set; }
    public virtual DbSet<Tarjeta> Tarjetas { get; set; }
    public virtual DbSet<Prestamo> Prestamos { get; set; }
    public virtual DbSet<Solicitud> Solicitudes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LaBancaUCBContext).Assembly);
    }
}