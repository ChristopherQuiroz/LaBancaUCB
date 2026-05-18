using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaBancaUCB.Services.Services;

public class SeguroService : ISeguroService
{
    private readonly IUnitOfWork _unitOfWork;

    public SeguroService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<Seguro>> GetAllSegurosAsync(PaginationFilter filters)
    {
        var todos = await _unitOfWork.SeguroRepository.GetAllAsync();
        var ordenados = todos.OrderByDescending(s => s.FechaInicio);
        return PagedList<Seguro>.Create(ordenados, filters.PageNumber, filters.PageSize);
    }

    public async Task<Seguro?> GetSeguroByIdAsync(long id)
    {
        return await _unitOfWork.SeguroRepository.GetByIdAsync(id);
    }

    public async Task InsertSeguroAsync(Seguro seguro)
    {
        await _unitOfWork.SeguroRepository.AddAsync(seguro);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateSeguroAsync(Seguro seguro)
    {
        var existente = await _unitOfWork.SeguroRepository.GetByIdAsync(seguro.IdSeguro);
        if (existente == null)
            throw new System.Exception("Seguro no encontrado");

        _unitOfWork.SeguroRepository.Update(seguro);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSeguroAsync(long id)
    {
        var seguro = await _unitOfWork.SeguroRepository.GetByIdAsync(id);
        if (seguro == null)
            throw new System.Exception("Seguro no encontrado");

        await _unitOfWork.SeguroRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
