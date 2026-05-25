using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class TarjetaConfiguration : IEntityTypeConfiguration<Tarjeta>
{
    public void Configure(EntityTypeBuilder<Tarjeta> builder)
    {
        builder.Ignore(t => t.Id);
        builder.HasKey(t => t.IdTarjeta);
        builder.ToTable("Tarjetas");

        builder.Property(t => t.IdTarjeta).HasColumnName("id_tarjeta");
        builder.Property(t => t.IdCuenta).HasColumnName("id_cuenta");
        builder.Property(t => t.Tipo).HasColumnName("tipo");
        builder.Property(t => t.NumeroEnmascarado).HasColumnName("numero_enmascarado");
        builder.Property(t => t.FechaVencimiento).HasColumnName("fecha_vencimiento");
        builder.Property(t => t.Estado).HasColumnName("estado");
        builder.Property(t => t.FechaCreacion).HasColumnName("fecha_creacion");

        builder.HasOne(t => t.IdCuentaNavigation).WithMany().HasForeignKey(t => t.IdCuenta);
    }
}
