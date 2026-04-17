using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Services.Interfaces;

public interface IBeneficiarioService
{
    Task<IEnumerable<Beneficiario>> GetAllBeneficiariosAsync();
    Task<Beneficiario?> GetBeneficiarioByIdAsync(long id);
    Task InsertBeneficiarioAsync(Beneficiario beneficiario);
    Task UpdateBeneficiarioAsync(Beneficiario beneficiario);
    Task DeleteBeneficiarioAsync(long id);
}
