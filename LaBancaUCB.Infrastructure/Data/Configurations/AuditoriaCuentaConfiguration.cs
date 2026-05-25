using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class AuditoriaCuentaConfiguration : IEntityTypeConfiguration<AuditoriaCuenta>
{
    public void Configure(EntityTypeBuilder<AuditoriaCuenta> builder)
    {
        builder.Ignore(a => a.Id); // Ignoramos el Id base
        builder.HasKey(a => a.IdAuditoriaCuenta);
        builder.ToTable("AuditoriaCuentas");

        builder.Property(a => a.IdAuditoriaCuenta).HasColumnName("id_auditoria_cuenta");
        builder.Property(a => a.IdCuenta).HasColumnName("id_cuenta");
        builder.Property(a => a.IdAdministrador).HasColumnName("id_administrador");
        builder.Property(a => a.Accion).HasColumnName("accion");
        builder.Property(a => a.Motivo).HasColumnName("motivo");
        builder.Property(a => a.IpOrigen).HasColumnName("ip_origen");
        builder.Property(a => a.EjecutadoEn).HasColumnName("ejecutado_en");
    }
}
