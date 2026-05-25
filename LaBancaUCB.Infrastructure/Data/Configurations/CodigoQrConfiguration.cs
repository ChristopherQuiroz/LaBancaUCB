using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class CodigoQrConfiguration : IEntityTypeConfiguration<CodigoQr>
{
    public void Configure(EntityTypeBuilder<CodigoQr> builder)
    {
        builder.Ignore(q => q.Id);
        builder.HasKey(q => q.IdQr);
        builder.ToTable("Codigos_qr");

        builder.Property(q => q.IdQr).HasColumnName("id_qr");
        builder.Property(q => q.IdCuentaReceptora).HasColumnName("id_cuenta_receptora");
        builder.Property(q => q.CodigoHash).HasColumnName("codigo_hash");
        builder.Property(q => q.MontoFijo).HasColumnName("monto_fijo");
        builder.Property(q => q.EsMontoVariable).HasColumnName("es_monto_variable");
        builder.Property(q => q.Descripcion).HasColumnName("descripcion");
        builder.Property(q => q.Activo).HasColumnName("activo");
        builder.Property(q => q.ExpiraEn).HasColumnName("expira_en");
        builder.Property(q => q.CreadoEn).HasColumnName("creado_en");

        builder.HasOne(q => q.IdCuentaReceptoraNavigation)
               .WithMany()
               .HasForeignKey(q => q.IdCuentaReceptora);
    }
}