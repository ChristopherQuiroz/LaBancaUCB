using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class PrestamoService : IPrestamoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PrestamoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Prestamo>> GetAllPrestamosAsync()
    {
        return await _unitOfWork.PrestamoRepository.GetAllAsync();
    }

    public async Task<Prestamo?> GetPrestamoByIdAsync(long id)
    {
        return await _unitOfWork.PrestamoRepository.GetByIdAsync(id);
    }

    public async Task InsertPrestamoAsync(Prestamo prestamo)
    {
        await _unitOfWork.PrestamoRepository.AddAsync(prestamo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePrestamoAsync(Prestamo prestamo)
    {
        var existente = await _unitOfWork.PrestamoRepository.GetByIdAsync(prestamo.IdPrestamo);
        if (existente == null) throw new System.Exception("Prestamo no encontrado");

        _unitOfWork.PrestamoRepository.Update(prestamo);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePrestamoAsync(long id)
    {
        var existente = await _unitOfWork.PrestamoRepository.GetByIdAsync(id);
        if (existente == null) throw new System.Exception("Prestamo no encontrado");

        await _unitOfWork.PrestamoRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
