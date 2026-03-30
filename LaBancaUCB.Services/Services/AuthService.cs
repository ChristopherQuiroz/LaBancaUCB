using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LaBancaUCB.Services.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<Usuario> _usuarioRepository;
    private readonly IBaseRepository<Sesione> _sesioneRepository; 
    private readonly IConfiguration _configuration;

    public AuthService(
        IBaseRepository<Usuario> usuarioRepository,
        IBaseRepository<Sesione> sesioneRepository, 
        IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _sesioneRepository = sesioneRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
    {
       
        var usuarios = await _usuarioRepository.GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u =>
            u.Email.ToLower() == loginDto.Email.ToLower());

       
        if (usuario == null)
            throw new Exception("Credenciales incorrectas");

       
        if (usuario.Activo == false)
            throw new Exception("La cuenta está inactiva");

        if (usuario.Bloqueado)
            throw new Exception("La cuenta está bloqueada");

       
        if (usuario.PasswordHash != loginDto.Password)
            throw new Exception("Credenciales incorrectas");

      
        var jti = Guid.NewGuid();
        var expiracion = DateTime.UtcNow.AddHours(8);

        
        var token = GenerarToken(usuario, jti, expiracion);

      
        var nuevaSesion = new Sesione
        {
            IdUsuario = usuario.IdUsuario,
            TokenJti = jti,
            IpOrigen = "Desconocida", 
            Activo = true,
            ExpiradoEn = expiracion,
            CreadoEn = DateTime.UtcNow
        };

        await _sesioneRepository.AddAsync(nuevaSesion);

        return new LoginResponseDto
        {
            Token = token,
            Email = usuario.Email,
            NombreCompleto = usuario.NombreCompleto,
            Rol = usuario.Rol,
            Expiracion = expiracion
        };
    }

    private string GenerarToken(Usuario usuario, Guid jti, DateTime expiracion)
    {
        var jwtKey = _configuration["Jwt:Key"]!;
        var jwtIssuer = _configuration["Jwt:Issuer"]!;
        var jwtAudience = _configuration["Jwt:Audience"]!;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()), 
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.NombreCompleto),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expiracion,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}