using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface IBeneficiarioService
{
    Task<PagedList<Beneficiario>> GetAllBeneficiariosAsync(long idUsuarioOwner, PaginationFilter filters);
    Task<Beneficiario?> GetBeneficiarioByIdAsync(long id);
    Task InsertBeneficiarioAsync(Beneficiario beneficiario);
    Task UpdateBeneficiarioAsync(Beneficiario beneficiario);
    Task DeleteBeneficiarioAsync(long id);
}