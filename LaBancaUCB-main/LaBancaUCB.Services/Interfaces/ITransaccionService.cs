using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface ITransaccionService
{
    Task<IEnumerable<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario);

    Task<Transaccion> CrearTransferenciaExteriorAsync(TransferenciaExteriorDto dto, long usuarioId);

    Task<IEnumerable<Transaccion>> ListarTransferenciasPorEstadoAsync(string? estado = null);

    Task AprobarTransferenciaAsync(long transaccionId, long adminId);

    Task RechazarTransferenciaAsync(long transaccionId, string motivo, long adminId);
}