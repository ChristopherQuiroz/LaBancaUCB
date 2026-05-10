using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface IPrestamoService
{
    Task<IEnumerable<Prestamo>> GetAllPrestamosAsync();
    Task<Prestamo?> GetPrestamoByIdAsync(long id);
    Task InsertPrestamoAsync(Prestamo prestamo);
    Task UpdatePrestamoAsync(Prestamo prestamo);
    Task DeletePrestamoAsync(long id);
}
