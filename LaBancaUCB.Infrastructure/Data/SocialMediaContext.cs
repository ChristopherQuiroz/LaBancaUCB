using LaBancaUCB.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Data;

public partial class SocialMediaContext : DbContext
{
    public SocialMediaContext()
    {
    }

    public SocialMediaContext(DbContextOptions<SocialMediaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beneficiario> Beneficiarios { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=labancaucb;uid=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Beneficiario>(entity =>
        {
            entity.HasKey(e => e.IdBeneficiario).HasName("PRIMARY");

            entity.ToTable("beneficiarios");

            entity.HasIndex(e => e.IdUsuarioOwner, "fk_beneficiario_usuario");

            entity.Property(e => e.IdBeneficiario).HasColumnName("id_beneficiario");
            entity.Property(e => e.Alias)
                .HasMaxLength(100)
                .HasColumnName("alias");
            entity.Property(e => e.BancoDestino)
                .HasMaxLength(100)
                .HasColumnName("banco_destino");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.EsExterior).HasColumnName("es_exterior");
            entity.Property(e => e.IdUsuarioOwner).HasColumnName("id_usuario_owner");
            entity.Property(e => e.NombreTitular)
                .HasMaxLength(200)
                .HasColumnName("nombre_titular");
            entity.Property(e => e.NumeroCuentaDestino)
                .HasMaxLength(20)
                .HasColumnName("numero_cuenta_destino");

            entity.HasOne(d => d.IdUsuarioOwnerNavigation).WithMany(p => p.Beneficiarios)
                .HasForeignKey(d => d.IdUsuarioOwner)
                .HasConstraintName("fk_beneficiario_usuario");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PRIMARY");

            entity.ToTable("cuentas");

            entity.HasIndex(e => e.IdUsuario, "fk_cuenta_usuario");

            entity.HasIndex(e => e.NumeroCuenta, "uk_numero_cuenta").IsUnique();

            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'activada'")
                .HasColumnType("enum('activada','bloqueada','cerrada')")
                .HasColumnName("estado");
            entity.Property(e => e.FechaApertura)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_apertura");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'BOB'")
                .IsFixedLength()
                .HasColumnName("moneda");
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(20)
                .HasColumnName("numero_cuenta");
            entity.Property(e => e.Saldo)
                .HasPrecision(15, 2)
                .HasColumnName("saldo");
            entity.Property(e => e.TipoCuenta)
                .HasColumnType("enum('ahorrado','corriente')")
                .HasColumnName("tipo_cuenta");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuenta_usuario");
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PRIMARY");

            entity.ToTable("sesiones");

            entity.HasIndex(e => e.IdUsuario, "fk_sesion_usuario");

            entity.HasIndex(e => e.TokenJti, "uk_token_jti").IsUnique();

            entity.Property(e => e.IdSesion).HasColumnName("id_sesion");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("creado_en");
            entity.Property(e => e.ExpiradoEn)
                .HasColumnType("timestamp")
                .HasColumnName("expirado_en");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IpOrigen)
                .HasMaxLength(45)
                .HasColumnName("ip_origen");
            entity.Property(e => e.TokenJti).HasColumnName("token_jti");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Sesiones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_sesion_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "uk_email").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Bloqueado).HasColumnName("bloqueado");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaDeCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_de_creacion");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(200)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(250)
                .HasColumnName("password_hash");
            entity.Property(e => e.Rol)
                .HasDefaultValueSql("'cliente'")
                .HasColumnType("enum('cliente','admin')")
                .HasColumnName("rol");
            entity.Property(e => e.UltimoLogin)
                .HasColumnType("timestamp")
                .HasColumnName("ultimo_login");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
