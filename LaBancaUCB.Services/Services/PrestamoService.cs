using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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


    public async Task<PagedList<Prestamo>> GetPrestamosPaginadosAsync(int pageNumber, int pageSize)
    {
        var todos = await _unitOfWork.PrestamoRepository.GetAllAsync();
        var ordenados = todos.OrderByDescending(p => p.SolicitadoEn);
        return PagedList<Prestamo>.Create(ordenados, pageNumber, pageSize);
    }

    public async Task ActualizarPrestamoAsync(long idPrestamo, GestionarPrestamoDto dto)
    {
        var prestamo = await _unitOfWork.PrestamoRepository.GetByIdAsync(idPrestamo);
        if (prestamo == null)
            throw new BusinessException("Préstamo no encontrado.", HttpStatusCode.NotFound);

        prestamo.MontoAprobado = dto.MontoAprobado;
        prestamo.TasaInteres = dto.TasaInteres;
        prestamo.Estado = dto.Estado;

        if (dto.Estado == "aprobado" && prestamo.AprobadoEn == null)
        {
            prestamo.AprobadoEn = DateTime.UtcNow;
            var cuenta = await _unitOfWork.CuentaRepository.GetByIdAsync(prestamo.IdCuenta);
            if (cuenta != null)
            {
                cuenta.Saldo += dto.MontoAprobado;
                _unitOfWork.CuentaRepository.Update(cuenta);
            }
        }

        _unitOfWork.PrestamoRepository.Update(prestamo);
        await _unitOfWork.SaveChangesAsync();
    }
}
