using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class BeneficiarioConfiguration : IEntityTypeConfiguration<Beneficiario>
{
    public void Configure(EntityTypeBuilder<Beneficiario> builder)
    {
        builder.HasKey(b => b.IdBeneficiario);
    }
}