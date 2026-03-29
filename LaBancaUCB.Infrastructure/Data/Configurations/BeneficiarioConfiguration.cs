using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class BeneficiarioConfiguration : IEntityTypeConfiguration<Beneficiario>
{
    public void Configure(EntityTypeBuilder<Beneficiario> builder)
    {
        builder.Ignore(b => b.Id);
        builder.HasKey(b => b.IdBeneficiario);
        builder.ToTable("Beneficiarios");

        builder.Property(b => b.IdBeneficiario).HasColumnName("id_beneficiario");
        builder.Property(b => b.IdUsuarioOwner).HasColumnName("id_usuario_owner");
        builder.Property(b => b.Alias).HasColumnName("alias");
        builder.Property(b => b.NumeroCuentaDestino).HasColumnName("numero_cuenta_destino");
        builder.Property(b => b.BancoDestino).HasColumnName("banco_destino");
        builder.Property(b => b.NombreTitular).HasColumnName("nombre_titular");
        builder.Property(b => b.EsExterior).HasColumnName("es_exterior");
        builder.Property(b => b.CreadoEn).HasColumnName("creado_en");
    }
}