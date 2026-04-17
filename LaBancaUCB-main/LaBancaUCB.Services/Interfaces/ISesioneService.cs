using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface ISesioneService
{
    Task<IEnumerable<Sesione>> GetAllSesionesAsync();
    Task<Sesione?> GetSesioneByIdAsync(long id);
    Task InsertSesioneAsync(Sesione sesione);
    Task UpdateSesioneAsync(Sesione sesione);
    Task DeleteSesioneAsync(long id);
}
