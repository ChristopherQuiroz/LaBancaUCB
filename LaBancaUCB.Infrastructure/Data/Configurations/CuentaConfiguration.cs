using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Infrastructure.Data.Configurations
{
    internal class CuentaConfiguration : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            // Clave primaria
            builder.HasKey(e => e.IdCuenta)
                   .HasName("PK_Cuenta");

            // Nombre de la tabla
            builder.ToTable("Cuenta");

            // Propiedades de texto
            builder.Property(e => e.NumeroCuenta)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.TipoCuenta)
                   .HasMaxLength(30)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.Moneda)
                   .HasMaxLength(3)      // Código ISO de moneda (USD, BOB, etc.)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired();

            // Propiedad decimal con precisión
            builder.Property(e => e.Saldo)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Fecha
            builder.Property(e => e.FechaApertura)
                   .IsRequired();

            // Relación con Usuario
            builder.HasOne(e => e.IdUsuarioNavigation)
                   .WithMany(u => u.Cuenta)
                   .HasForeignKey(e => e.IdUsuario)
                   .HasConstraintName("FK_Cuenta_Usuario")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
