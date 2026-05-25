using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class TransaccionConfiguration : IEntityTypeConfiguration<Transaccion>
{
    public void Configure(EntityTypeBuilder<Transaccion> builder)
    {
        builder.Ignore(t => t.Id);
        builder.HasKey(t => t.IdTransaccion);
        builder.ToTable("Transacciones");

        builder.Property(t => t.IdTransaccion).HasColumnName("id_transaccion");
        builder.Property(t => t.IdCuentaOrigen).HasColumnName("id_cuenta_origen");
        builder.Property(t => t.IdCuentaDestino).HasColumnName("id_cuenta_destino");
        builder.Property(t => t.NombreDestino).HasColumnName("nombre_destino");
        builder.Property(t => t.Tipo).HasColumnName("tipo");
        builder.Property(t => t.Monto).HasColumnName("monto");
        builder.Property(t => t.Moneda).HasColumnName("moneda");
        builder.Property(t => t.TipoCambio).HasColumnName("tipo_cambio");
        builder.Property(t => t.Glosa).HasColumnName("glosa");
        builder.Property(t => t.Estado).HasColumnName("estado");
        builder.Property(t => t.ReferenciaQr).HasColumnName("referencia_qr");
        builder.Property(t => t.FechaHora).HasColumnName("fecha_hora");
        builder.Property(t => t.IdBeneficiario).HasColumnName("id_beneficiario");

        builder.HasOne(t => t.IdCuentaOrigenNavigation)
               .WithMany()
               .HasForeignKey(t => t.IdCuentaOrigen);

        builder.HasOne(t => t.IdBeneficiarioNavigation)
               .WithMany()
               .HasForeignKey(t => t.IdBeneficiario);
    }
}