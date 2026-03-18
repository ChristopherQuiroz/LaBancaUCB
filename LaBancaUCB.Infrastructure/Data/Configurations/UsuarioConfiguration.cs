using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.IdUsuario);
        builder.ToTable("Usuarios");

        builder.Property(u => u.IdUsuario).HasColumnName("id_usuario");
        builder.Property(u => u.Email).HasColumnName("email");
        builder.Property(u => u.NombreCompleto).HasColumnName("nombre_completo");
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
        builder.Property(u => u.Rol).HasColumnName("rol");
        builder.Property(u => u.Activo).HasColumnName("activo");
        builder.Property(u => u.Bloqueado).HasColumnName("bloqueado");
        builder.Property(u => u.FechaDeCreacion).HasColumnName("fecha_de_creacion");
        builder.Property(u => u.UltimoLogin).HasColumnName("ultimo_login");
    }
}