using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
namespace LaBancaUCB.Services.Services;

public class BeneficiarioService
{
    private readonly IBaseRepository<Beneficiario> _beneficiarioRepository;
    public BeneficiarioService(IBaseRepository<Beneficiario> beneficiarioRepository)
    {
        _beneficiarioRepository = beneficiarioRepository;
    }
    public async Task<IEnumerable<Beneficiario>> GetAllBeneficiariosAsync()
    {
        return await _beneficiarioRepository.GetAllAsync();
    }
    public async Task<Beneficiario?> GetBeneficiarioByIdAsync(long id)
    {
        return await _beneficiarioRepository.GetByIdAsync(id);
    }
    public async Task InsertBeneficiarioAsync(Beneficiario beneficiario)
    {
        await _beneficiarioRepository.AddAsync(beneficiario);
    }
    public async Task UpdateBeneficiarioAsync(Beneficiario beneficiario)
    {
        var beneficiarioExistente = _beneficiarioRepository.GetByIdAsync(beneficiario.Id);
        if (beneficiarioExistente == null)
        {
            throw new Exception("Beneficiario no encontrado");
        }
        await _beneficiarioRepository.UpdateAsync(beneficiario);
    }
    public async Task DeleteBeneficiarioAsync(long id)
    {
        var beneficiario = _beneficiarioRepository.GetByIdAsync(id);
        if (beneficiario == null)
        {
            throw new Exception("Beneficiario no encontrado");
        }
        await _beneficiarioRepository.DeleteAsync(id);
    }
}
