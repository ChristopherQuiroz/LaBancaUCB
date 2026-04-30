using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface IAdminSolicitudesService
{
    Task<IEnumerable<Solicitud>> GetSolicitudesAsync(string? estado);
    Task<Solicitud?> GetSolicitudByIdAsync(long id);
    Task GestionarSolicitudAsync(long id, GestionarSolicitudDto dto);
}
