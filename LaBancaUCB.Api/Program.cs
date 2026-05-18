using FluentValidation;
using FluentValidation.AspNetCore;
using LaBancaUCB.Core.Entities;
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

namespace LaBancaUCB.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ISecurityService, SecurityService>();
        builder.Services.AddScoped<ICuentaService, CuentaService>();
        builder.Services.AddScoped<ITransaccionService, TransaccionService>();
        builder.Services.AddScoped<IBeneficiarioService, BeneficiarioService>();
        builder.Services.AddScoped<IProductosService, ProductosService>();
        builder.Services.AddScoped<IAdminSolicitudesService, AdminSolicitudesService>();
        builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        builder.Services.AddScoped<IDapperContext, DapperContext>();
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
            }
            ).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "LaBancaUCB API",
                Version = "v1",
                Description = "Backend LaBancaUCB",
                Contact = new()
                {
                    Name = "LaBancaUCB Team",
                    Email = "ChrisVictor@ucb.edu.bo"
                }
            });


            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.EnableAnnotations();
        });

        builder.Services.AddDbContext<LaBancaUCBContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
            )
        );

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!))
            };
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseMiddleware<LaBancaUCB.Api.Filters.ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LaBancaUCB API v1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}