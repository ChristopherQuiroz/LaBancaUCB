using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class BeneficiarioService : IBeneficiarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public BeneficiarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Beneficiario>> GetAllBeneficiariosAsync()
    {
        return await _unitOfWork.BeneficiarioRepository.GetAllAsync();
    }

    public async Task<Beneficiario?> GetBeneficiarioByIdAsync(long id)
    {
        return await _unitOfWork.BeneficiarioRepository.GetByIdAsync(id);
    }

    public async Task InsertBeneficiarioAsync(Beneficiario beneficiario)
    {
        await _unitOfWork.BeneficiarioRepository.AddAsync(beneficiario);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateBeneficiarioAsync(Beneficiario beneficiario)
    {
        var beneficiarioExistente = await _unitOfWork.BeneficiarioRepository.GetByIdAsync(beneficiario.Id);
        if (beneficiarioExistente == null)
        {
            throw new Exception("Beneficiario no encontrado");
        }

        _unitOfWork.BeneficiarioRepository.Update(beneficiario);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteBeneficiarioAsync(long id)
    {
        var beneficiario = await _unitOfWork.BeneficiarioRepository.GetByIdAsync(id);
        if (beneficiario == null)
        {
            throw new Exception("Beneficiario no encontrado");
        }
        await _unitOfWork.BeneficiarioRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}