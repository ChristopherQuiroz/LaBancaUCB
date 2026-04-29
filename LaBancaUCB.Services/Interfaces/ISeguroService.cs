using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface ISeguroService
{
    Task<IEnumerable<Seguro>> GetAllSegurosAsync();
    Task<Seguro?> GetSeguroByIdAsync(long id);
    Task InsertSeguroAsync(Seguro seguro);
    Task UpdateSeguroAsync(Seguro seguro);
    Task DeleteSeguroAsync(long id);
}
