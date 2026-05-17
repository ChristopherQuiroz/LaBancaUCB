using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface ISeguroService
{
    Task<PagedList<Seguro>> GetAllSegurosAsync(PaginationFilter filters);
    Task<Seguro?> GetSeguroByIdAsync(long id);
    Task InsertSeguroAsync(Seguro seguro);
    Task UpdateSeguroAsync(Seguro seguro);
    Task DeleteSeguroAsync(long id);
}