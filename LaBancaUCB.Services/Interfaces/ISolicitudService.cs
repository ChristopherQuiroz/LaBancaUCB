using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface ISolicitudService
{
    Task<PagedList<Solicitud>> GetAllSolicitudesAsync(PaginationFilter filters);
    Task<Solicitud?> GetSolicitudByIdAsync(long id);
    Task InsertSolicitudAsync(Solicitud solicitud);
    Task UpdateSolicitudAsync(Solicitud solicitud);
    Task DeleteSolicitudAsync(long id);
}