using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Services.Interfaces;

public interface IAdminSolicitudesService
{
    Task<PagedList<Solicitud>> GetSolicitudesAsync(SolicitudQueryFilter filters);
    Task<Solicitud?> GetSolicitudByIdAsync(long id);
    Task GestionarSolicitudAsync(long id, GestionarSolicitudDto dto);
}