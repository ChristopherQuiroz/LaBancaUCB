using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces;

public interface ISesioneRepository
{
    Task<IEnumerable<Sesione>> GetAllSesionesAsync();
    Task<Sesione?> GetSesioneByIdAsync(long id);
    Task InsertSesioneAsync(Sesione sesione);
    Task UpdateSesioneAsync(Sesione sesione);
    Task DeleteSesioneAsync(Sesione sesione);
}
