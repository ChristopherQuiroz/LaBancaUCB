using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Infrastructure.Data.Configurations
{
    internal class SesioneConfiguration : IEntityTypeConfiguration<Sesione>
    {
        public void Configure(EntityTypeBuilder<Sesione> builder)
        {
            // Clave primaria
            builder.HasKey(e => e.IdSesion)
                   .HasName("PK_Sesione");

            // Nombre de la tabla
            builder.ToTable("Sesione");

            // Propiedades
            builder.Property(e => e.IpOrigen)
                   .HasMaxLength(45)            // IPv6 puede tener hasta 45 caracteres
                   .IsUnicode(false);

            builder.Property(e => e.Activo)
                   .HasDefaultValue(true);

            builder.Property(e => e.ExpiradoEn)
                   .IsRequired();

            builder.Property(e => e.CreadoEn)
                   .IsRequired();

            // Relación con Usuario
            builder.HasOne(e => e.IdUsuarioNavigation)
                   .WithMany(u => u.Sesiones)
                   .HasForeignKey(e => e.IdUsuario)
                   .HasConstraintName("FK_Sesione_Usuario")
                   .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada
        }
    }
}
