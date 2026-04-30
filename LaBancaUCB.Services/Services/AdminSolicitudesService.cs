using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class AdminSolicitudesService : IAdminSolicitudesService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminSolicitudesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Solicitud>> GetSolicitudesAsync(string? estado)
    {
        var todas = await _unitOfWork.SolicitudRepository.GetAllAsync();

        if (string.IsNullOrWhiteSpace(estado))
            return todas.OrderByDescending(s => s.FechaCreacion);

        return todas.Where(s => s.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(s => s.FechaCreacion);
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