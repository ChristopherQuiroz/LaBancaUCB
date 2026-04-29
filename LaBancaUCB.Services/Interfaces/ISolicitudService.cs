using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface ISolicitudService
{
    Task<IEnumerable<Solicitud>> GetAllSolicitudesAsync();
    Task<Solicitud?> GetSolicitudByIdAsync(long id);
    Task InsertSolicitudAsync(Solicitud solicitud);
    Task UpdateSolicitudAsync(Solicitud solicitud);
    Task DeleteSolicitudAsync(long id);
}
