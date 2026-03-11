using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.IdUsuario)
                .HasName("PK_Usuario");

            builder.ToTable("Usuario");

            builder.Property(e => e.Email)
               .HasMaxLength(100)
               .IsUnicode(false)
               .IsRequired();

            builder.Property(e => e.NombreCompleto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Activo)
                .HasDefaultValue(true);

            builder.Property(e => e.Bloqueado)
                .IsRequired();

            builder.Property(e => e.FechaDeCreacion)
                .IsRequired();

            builder.Property(e => e.UltimoLogin)
                .IsRequired(false);
        }
    }
}
