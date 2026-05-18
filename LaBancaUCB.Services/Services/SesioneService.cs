using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Services;

public class SesioneService : ISesioneService
{
    private readonly IUnitOfWork _unitOfWork;

    public SesioneService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<Sesione>> GetAllSesionesAsync(PaginationFilter filters)
    {
        var todos = await _unitOfWork.SesioneRepository.GetAllAsync();
        var ordenados = todos.OrderByDescending(s => s.CreadoEn);
        return PagedList<Sesione>.Create(ordenados, filters.PageNumber, filters.PageSize);
    }

    public async Task<Sesione?> GetSesioneByIdAsync(long id)
    {
        return await _unitOfWork.SesioneRepository.GetByIdAsync(id);
    }

    public async Task InsertSesioneAsync(Sesione sesione)
    {
        await _unitOfWork.SesioneRepository.AddAsync(sesione);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateSesioneAsync(Sesione sesione)
    {
        var sesioneExistente = await _unitOfWork.SesioneRepository.GetByIdAsync(sesione.Id);
        if (sesioneExistente == null)
        {
            throw new Exception("Sesión no encontrada");
        }

        _unitOfWork.SesioneRepository.Update(sesione);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSesioneAsync(long id)
    {
        var sesione = await _unitOfWork.SesioneRepository.GetByIdAsync(id);
        if (sesione == null)
        {
            throw new Exception("Sesión no encontrada");
        }
        await _unitOfWork.SesioneRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}