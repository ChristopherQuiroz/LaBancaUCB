using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface ICuentaService
{
    Task<PagedList<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario, PaginationFilter filters);
}