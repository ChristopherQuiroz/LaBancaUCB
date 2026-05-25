using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LaBancaUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;

        public TokenController(IConfiguration configuration,
            ISecurityService securityService)
        {
            _configuration = configuration;
            _securityService = securityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            // Validar que el usuario sea válido
            var validation = await IsValidUser(userLogin);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private async Task<(bool, Security)> IsValidUser(UserLogin userLogin)
        {
            var user = await _securityService.GetLoginByCredentials(userLogin);
            var isValid = _passwordService.Check(user.Password, userLogin.Password);
            return (isValid, user);
        }

        private string GenerateToken(Security user)
        {
            // HEADER
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration["Authentication:SecretKey"]!));
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // PAYLOAD (Cuerpo)
            var claims = new[]
            {
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Email, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var payload =
                new JwtPayload(
                    // Quién emite el token
                    issuer: _configuration["Authentication:Issuer"],

                    // Quién va a recibir el token
                    audience: _configuration["Authentication:Audience"],

                    // Los datos del usuario
                    claims: claims,

                    // Desde cuándo es válido el token
                    notBefore: DateTime.UtcNow,

                    // Cuándo expira
                    expires: DateTime.UtcNow.AddMinutes(2)
                );

            // FIRMA
            var token = new JwtSecurityToken(header, payload);

            // Serializar el token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}