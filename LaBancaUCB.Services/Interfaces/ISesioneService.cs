using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface ISesioneService
{
    Task<PagedList<Sesione>> GetAllSesionesAsync(PaginationFilter filters);
    Task<Sesione?> GetSesioneByIdAsync(long id);
    Task InsertSesioneAsync(Sesione sesione);
    Task UpdateSesioneAsync(Sesione sesione);
    Task DeleteSesioneAsync(long id);
}
