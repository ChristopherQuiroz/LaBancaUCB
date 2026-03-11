using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Infrastructure.Data.Configurations
{
    internal class BeneficiarioConfiguration : IEntityTypeConfiguration<Beneficiario>
    {
        public void Configure(EntityTypeBuilder<Beneficiario> builder)
        {
            // Clave primaria
            builder.HasKey(e => e.IdBeneficiario)
                   .HasName("PK_Beneficiario");

            // Nombre de la tabla
            builder.ToTable("Beneficiario");

            // Propiedades de texto
            builder.Property(e => e.Alias)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.NumeroCuentaDestino)
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(e => e.BancoDestino)
                   .HasMaxLength(100)
                   .IsUnicode(false);  // Puede ser nulo

            builder.Property(e => e.NombreTitular)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .IsRequired();

            // Booleano
            builder.Property(e => e.EsExterior)
                   .IsRequired();

            // Fecha
            builder.Property(e => e.CreadoEn)
                   .IsRequired();

            // Relación con Usuario (owner)
            builder.HasOne(e => e.IdUsuarioOwnerNavigation)
                   .WithMany(u => u.Beneficiarios)
                   .HasForeignKey(e => e.IdUsuarioOwner)
                   .HasConstraintName("FK_Beneficiario_Usuario")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
