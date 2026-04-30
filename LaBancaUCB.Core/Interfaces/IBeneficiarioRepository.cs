using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces;

public interface IBeneficiarioRepository
{
    Task<IEnumerable<Beneficiario>> GetAllBeneficiariosAsync();
    Task<Beneficiario?> GetBeneficiarioByIdAsync(long id);
    Task InsertBeneficiarioAsync(Beneficiario beneficiario);
    Task UpdateBeneficiarioAsync(Beneficiario beneficiario);
    Task DeleteBeneficiarioAsync(Beneficiario beneficiario);
}
