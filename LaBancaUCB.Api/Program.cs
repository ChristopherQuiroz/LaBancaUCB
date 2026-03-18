using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using LaBancaUCB.Infrastructure.Mappings;
using LaBancaUCB.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();


builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);


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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();