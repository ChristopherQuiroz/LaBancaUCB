using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class AdministradorConfiguration : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.Ignore(a => a.Id);
        builder.HasKey(a => a.IdAdministrador);
        builder.ToTable("Administradores");

        builder.Property(a => a.IdAdministrador).HasColumnName("id_administrador");
        builder.Property(a => a.IdUsuario).HasColumnName("id_usuario");
        builder.Property(a => a.NivelAcceso).HasColumnName("nivel_acceso");
        builder.Property(a => a.Departamento).HasColumnName("departamento");
        builder.Property(a => a.Activo).HasColumnName("activo");
        builder.Property(a => a.AsignadoEn).HasColumnName("asignado_en");

        builder.HasOne<Usuario>()
               .WithMany()
               .HasForeignKey(a => a.IdUsuario)
               .HasConstraintName("FK_Administradores_Usuario")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
