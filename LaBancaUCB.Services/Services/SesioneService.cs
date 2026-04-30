using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class SesioneService : ISesioneService
{
    private readonly IUnitOfWork _unitOfWork;

    public SesioneService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Sesione>> GetAllSesionesAsync()
    {
        return await _unitOfWork.SesioneRepository.GetAllAsync();
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