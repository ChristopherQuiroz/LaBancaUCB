using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class SolicitudConfiguration : IEntityTypeConfiguration<Solicitud>
{
    public void Configure(EntityTypeBuilder<Solicitud> builder)
    {
        builder.Ignore(s => s.Id);
        builder.HasKey(s => s.IdSolicitud);
        builder.ToTable("Solicitudes");

        builder.Property(s => s.IdSolicitud).HasColumnName("id_solicitud");
        builder.Property(s => s.IdUsuario).HasColumnName("id_usuario");
        builder.Property(s => s.TipoSolicitud).HasColumnName("tipo_solicitud");
        builder.Property(s => s.referenciaID).HasColumnName("referencia_id");
        builder.Property(s => s.Estado).HasColumnName("estado");
        builder.Property(s => s.IdAdmin).HasColumnName("id_admin");
        builder.Property(s => s.ObservacionAdmin).HasColumnName("observacion_admin");
        builder.Property(s => s.FechaCreacion).HasColumnName("fecha_creacion");
        builder.Property(s => s.GestionadaEn).HasColumnName("gestionada_en");

        builder.HasOne(s => s.IdUsuarioNavigation)
               .WithMany(u => u.Solicitudes)
               .HasForeignKey(s => s.IdUsuario)
               .HasConstraintName("FK_Solicitudes_Usuario")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
