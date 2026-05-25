using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class SolicitudService : ISolicitudService
{
    private readonly IUnitOfWork _unitOfWork;

    public SolicitudService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Solicitud>> GetAllSolicitudesAsync()
    {
        return await _unitOfWork.SolicitudRepository.GetAllAsync();
    }

    public async Task<Solicitud?> GetSolicitudByIdAsync(long id)
    {
        return await _unitOfWork.SolicitudRepository.GetByIdAsync(id);
    }

    public async Task InsertSolicitudAsync(Solicitud solicitud)
    {
        await _unitOfWork.SolicitudRepository.AddAsync(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateSolicitudAsync(Solicitud solicitud)
    {
        var existente = await _unitOfWork.SolicitudRepository.GetByIdAsync(solicitud.IdSolicitud);
        if (existente == null) throw new System.Exception("Solicitud no encontrada");

        _unitOfWork.SolicitudRepository.Update(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSolicitudAsync(long id)
    {
        var existente = await _unitOfWork.SolicitudRepository.GetByIdAsync(id);
        if (existente == null) throw new System.Exception("Solicitud no encontrada");

        await _unitOfWork.SolicitudRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
