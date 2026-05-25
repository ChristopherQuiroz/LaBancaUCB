using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Interfaces;

public interface IPrestamoService
{
    Task<IEnumerable<Prestamo>> GetAllPrestamosAsync();
    Task<Prestamo?> GetPrestamoByIdAsync(long id);
    Task InsertPrestamoAsync(Prestamo prestamo);
    Task UpdatePrestamoAsync(Prestamo prestamo);
    Task DeletePrestamoAsync(long id);

    Task<PagedList<Prestamo>> GetPrestamosPaginadosAsync(int pageNumber, int pageSize);
    Task ActualizarPrestamoAsync(long idPrestamo, GestionarPrestamoDto dto);
}
