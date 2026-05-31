using FluentValidation;
using FluentValidation.AspNetCore;
using LaBancaUCB.Infrastructure.Services;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using LaBancaUCB.Infrastructure.Mappings;
using LaBancaUCB.Infrastructure.Repositories;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Services.Services;
using LaBancaUCB.Services.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LaBancaUCB.Core.CustomEntities;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.Sources.Clear();
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ICuentaService, CuentaService>();
        builder.Services.AddScoped<IPrestamoService, PrestamoService>();
        builder.Services.AddScoped<ITransaccionService, TransaccionService>();
        builder.Services.AddScoped<IBeneficiarioService, BeneficiarioService>();
        builder.Services.AddScoped<IProductosService, ProductosService>();
        builder.Services.AddScoped<IAdminSolicitudesService, AdminSolicitudesService>();
        builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        builder.Services.AddScoped<IDapperContext, DapperContext>();
        builder.Services.AddScoped<IEmailService, SmtpEmailService>();
        builder.Services.AddScoped<IPagoQrService, PagoQrService>();
        builder.Services.AddScoped<IPasswordService, PasswordService>();

        builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<UsuarioDtoValidator>();
        builder.Services.AddScoped<CrearUsuarioValidator>();
        builder.Services.AddScoped<ActualizarUsuarioValidator>();
        builder.Services.AddScoped<LoginDtoValidator>();
        builder.Services.AddScoped<CrearBeneficiarioValidator>();

        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<LaBancaUCBContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
        );

        builder.Services.Configure<PasswordOptions>(
            builder.Configuration.GetSection("PasswordOptions"));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "LaBancaUCB API",
                Version = "v1",
                Description = "API para la gestión de la banca en línea de LaBancaUCB"
            });

            var xmlApi = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlApiPath = Path.Combine(AppContext.BaseDirectory, xmlApi);
            options.IncludeXmlComments(xmlApiPath);

            var xmlCore = "LaBancaUCB.Core.xml";
            var xmlCorePath = Path.Combine(AppContext.BaseDirectory, xmlCore);
            options.IncludeXmlComments(xmlCorePath);

            options.EnableAnnotations();
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();
        
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LaBancaUCB API v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseMiddleware<LaBancaUCB.Api.Filters.ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}