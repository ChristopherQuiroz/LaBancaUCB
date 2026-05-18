using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Services.Interfaces;

public interface IBeneficiarioService
{
    Task<PagedList<Beneficiario>> GetAllBeneficiariosAsync(long idUsuarioOwner, PaginationFilter filters);
    Task<Beneficiario?> GetBeneficiarioByIdAsync(long id);
    Task InsertBeneficiarioAsync(Beneficiario beneficiario);
    Task UpdateBeneficiarioAsync(Beneficiario beneficiario);
    Task DeleteBeneficiarioAsync(long id);
}
