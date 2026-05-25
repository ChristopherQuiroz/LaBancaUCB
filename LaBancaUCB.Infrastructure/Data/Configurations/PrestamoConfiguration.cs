using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class PrestamoConfiguration : IEntityTypeConfiguration<Prestamo>
{
    public void Configure(EntityTypeBuilder<Prestamo> builder)
    {
        builder.Ignore(p => p.Id);
        builder.HasKey(p => p.IdPrestamo);
        builder.ToTable("Prestamos");

        builder.Property(p => p.IdPrestamo).HasColumnName("id_prestamo");
        builder.Property(p => p.IdCuenta).HasColumnName("id_cuenta");
        builder.Property(p => p.MontoSolicitado).HasColumnName("monto_solicitado");
        builder.Property(p => p.MontoAprobado).HasColumnName("monto_aprobado");
        builder.Property(p => p.TasaInteres).HasColumnName("tasa_interes");
        builder.Property(p => p.PlazoMeses).HasColumnName("plazo_meses");
        builder.Property(p => p.Estado).HasColumnName("estado");
        builder.Property(p => p.SolicitadoEn).HasColumnName("solicitado_en");
        builder.Property(p => p.AprobadoEn).HasColumnName("aprobado_en");

        builder.HasOne(p => p.IdCuentaNavigation).WithMany().HasForeignKey(p => p.IdCuenta);
    }
}