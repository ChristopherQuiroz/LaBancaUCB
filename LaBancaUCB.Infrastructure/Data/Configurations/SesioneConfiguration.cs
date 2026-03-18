using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaBancaUCB.Infrastructure.Data.Configurations;

public class SesioneConfiguration : IEntityTypeConfiguration<Sesione>
{
    public void Configure(EntityTypeBuilder<Sesione> builder)
    {
        builder.HasKey(s => s.IdSesion);
    }
}