using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Services.Services;

public class AdminSolicitudesService : IAdminSolicitudesService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminSolicitudesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<Solicitud>> GetSolicitudesAsync(SolicitudQueryFilter filters)
    {
        var todas = await _unitOfWork.SolicitudRepository.GetAllAsync();
        var query = todas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Estado))
        {
            query = query.Where(s => s.Estado.Equals(filters.Estado, StringComparison.OrdinalIgnoreCase));
        }

        var solicitudesOrdenadas = query.OrderByDescending(s => s.FechaCreacion);

        var pagedSolicitudes = PagedList<Solicitud>.Create(solicitudesOrdenadas, filters.PageNumber, filters.PageSize);

        return pagedSolicitudes;
    }

    public async Task<Solicitud?> GetSolicitudByIdAsync(long id)
    {
        return await _unitOfWork.SolicitudRepository.GetByIdAsync(id);
    }

    public async Task GestionarSolicitudAsync(long id, GestionarSolicitudDto dto)
    {
        var solicitud = await _unitOfWork.SolicitudRepository.GetByIdAsync(id);
        if (solicitud == null)
            throw new BusinessException("Solicitud no encontrada.", HttpStatusCode.NotFound);

        solicitud.Estado = dto.Estado;
        solicitud.ObservacionAdmin = dto.ObservacionAdmin;
        solicitud.GestionadaEn = DateTime.UtcNow;

        _unitOfWork.SolicitudRepository.Update(solicitud);
        await _unitOfWork.SaveChangesAsync();
    }
}