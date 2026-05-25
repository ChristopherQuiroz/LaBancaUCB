using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
namespace LaBancaUCB.Services.Services;

public class SesioneService
{
    private readonly IBaseRepository<Sesione> _sesioneRepository;
    public SesioneService(IBaseRepository<Sesione> sesioneRepository)
    {
        _sesioneRepository = sesioneRepository;
    }
    public async Task<IEnumerable<Sesione>> GetAllSesionesAsync()
    {
        return await _sesioneRepository.GetAllAsync();
    }
    public async Task<Sesione?> GetSesioneByIdAsync(long id)
    {
        return await _sesioneRepository.GetByIdAsync(id);
    }
    public async Task InsertSesioneAsync(Sesione sesione)
    {
        await _sesioneRepository.AddAsync(sesione);
    }
    public async Task UpdateSesioneAsync(Sesione sesione)
    {
        var sesioneExistente = _sesioneRepository.GetByIdAsync(sesione.Id);
        if (sesioneExistente == null)
        {
            throw new Exception("Sesione no encontrado");
        }
        await _sesioneRepository.UpdateAsync(sesione);
    }
    public async Task DeleteSesioneAsync(long id)
    {
        var sesione = _sesioneRepository.GetByIdAsync(id);
        if (sesione == null)
        {
            throw new Exception("Sesione no encontrado");
        }
        await _sesioneRepository.DeleteAsync(id);
    }
}
