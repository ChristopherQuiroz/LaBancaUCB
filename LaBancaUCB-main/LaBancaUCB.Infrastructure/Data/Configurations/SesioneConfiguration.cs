using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class SesioneConfiguration : IEntityTypeConfiguration<Sesione>
{
    public void Configure(EntityTypeBuilder<Sesione> builder)
    {
        builder.Ignore(s => s.Id);
        builder.HasKey(s => s.IdSesion);
        builder.ToTable("Sesiones");

        builder.Property(s => s.IdSesion).HasColumnName("id_sesion");
        builder.Property(s => s.IdUsuario).HasColumnName("id_usuario");
        builder.Property(s => s.TokenJti).HasColumnName("token_jti");
        builder.Property(s => s.IpOrigen).HasColumnName("ip_origen");
        builder.Property(s => s.Activo).HasColumnName("activo");
        builder.Property(s => s.ExpiradoEn).HasColumnName("expirado_en");
        builder.Property(s => s.CreadoEn).HasColumnName("creado_en");

        builder.HasOne(s => s.IdUsuarioNavigation)
               .WithMany(u => u.Sesiones)
               .HasForeignKey(s => s.IdUsuario);
    }
}