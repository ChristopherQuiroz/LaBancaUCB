using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Interfaces;

public interface ISeguroService
{
    Task<PagedList<Seguro>> GetAllSegurosAsync(PaginationFilter filters);
    Task<Seguro?> GetSeguroByIdAsync(long id);
    Task InsertSeguroAsync(Seguro seguro);
    Task UpdateSeguroAsync(Seguro seguro);
    Task DeleteSeguroAsync(long id);
}
