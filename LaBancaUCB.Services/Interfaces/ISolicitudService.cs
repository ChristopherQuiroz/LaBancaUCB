using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Interfaces;

public interface ISolicitudService
{
    Task<PagedList<Solicitud>> GetAllSolicitudesAsync(PaginationFilter filters);
    Task<Solicitud?> GetSolicitudByIdAsync(long id);
    Task InsertSolicitudAsync(Solicitud solicitud);
    Task UpdateSolicitudAsync(Solicitud solicitud);
    Task DeleteSolicitudAsync(long id);
}
