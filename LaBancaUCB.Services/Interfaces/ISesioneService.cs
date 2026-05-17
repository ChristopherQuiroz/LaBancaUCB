using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface ISesioneService
{
    Task<PagedList<Sesione>> GetAllSesionesAsync(PaginationFilter filters);
    Task<Sesione?> GetSesioneByIdAsync(long id);
    Task InsertSesioneAsync(Sesione sesione);
    Task UpdateSesioneAsync(Sesione sesione);
    Task DeleteSesioneAsync(long id);
}