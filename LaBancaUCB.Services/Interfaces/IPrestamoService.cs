using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Interfaces;

public interface IPrestamoService
{
    Task<PagedList<Prestamo>> GetAllPrestamosAsync(PaginationFilter filters);
    Task<Prestamo?> GetPrestamoByIdAsync(long id);
    Task InsertPrestamoAsync(Prestamo prestamo);
    Task UpdatePrestamoAsync(Prestamo prestamo);
    Task DeletePrestamoAsync(long id);
}
