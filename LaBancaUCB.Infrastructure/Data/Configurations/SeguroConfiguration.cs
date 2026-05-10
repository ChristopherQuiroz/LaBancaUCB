using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class SeguroConfiguration : IEntityTypeConfiguration<Seguro>
{
    public void Configure(EntityTypeBuilder<Seguro> builder)
    {
        builder.Ignore(s => s.Id);
        builder.HasKey(s => s.IdSeguro);
        builder.ToTable("Seguros");

        builder.Property(s => s.IdSeguro).HasColumnName("id_seguro");
        builder.Property(s => s.IdCuenta).HasColumnName("id_cuenta");
        builder.Property(s => s.TipoSeguro).HasColumnName("tipo_seguro");
        builder.Property(s => s.PrimaMensual).HasColumnName("prima_mensual");
        builder.Property(s => s.Cobertura).HasColumnName("cobertura");
        builder.Property(s => s.Estado).HasColumnName("estado");
        builder.Property(s => s.FechaInicio).HasColumnName("fecha_inicio");
        builder.Property(s => s.FechaFin).HasColumnName("fecha_fin");

        builder.HasOne(s => s.IdCuentaNavigation).WithMany().HasForeignKey(s => s.IdCuenta);
    }
}
