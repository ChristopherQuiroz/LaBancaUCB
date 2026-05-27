using AutoMapper;
using LaBancaUCB.Api.Response;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaBancaUCB.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public SecurityController(ISecurityService securityService,
            IMapper mapper,
            IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// El password será hasheado antes de persistir la entidad.
        /// </remarks>
        /// <param name="securityDto">DTO con la información del usuario a registrar.</param>
        /// <returns>DTO del usuario registrado.</returns>
        /// <response code="200">Usuario registrado correctamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="500">Error interno del servidor</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<SecurityDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SecurityDto securityDto)
        {
            if (securityDto is null) return BadRequest(new { message = "Request body is required." });

            var security = _mapper.Map<Security>(securityDto);
            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);

            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }

}
