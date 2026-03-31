using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
{
    public void Configure(EntityTypeBuilder<Cuenta> builder)
    {
        builder.Ignore(c => c.Id);
        builder.HasKey(c => c.IdCuenta);
        builder.ToTable("Cuentas");

        builder.Property(c => c.IdCuenta).HasColumnName("id_cuenta");
        builder.Property(c => c.IdUsuario).HasColumnName("id_usuario");
        builder.Property(c => c.NumeroCuenta).HasColumnName("numero_cuenta");
        builder.Property(c => c.TipoCuenta).HasColumnName("tipo_cuenta");
        builder.Property(c => c.Saldo).HasColumnName("saldo");
        builder.Property(c => c.Moneda).HasColumnName("moneda");
        builder.Property(c => c.Estado).HasColumnName("estado");
        builder.Property(c => c.FechaApertura).HasColumnName("fecha_apertura");

        builder.HasOne(c => c.IdUsuarioNavigation)
               .WithMany(u => u.Cuenta)
               .HasForeignKey(c => c.IdUsuario);
    }
}