using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Services.Interfaces;

public interface ITransaccionService
{
    Task<PagedList<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario, TransaccionQueryFilter? filters = null);

    Task<Transaccion> CrearTransferenciaExteriorAsync(TransferenciaExteriorDto dto, long usuarioId);

    Task<IEnumerable<Transaccion>> ListarTransferenciasPorEstadoAsync(string? estado = null);

    Task AprobarTransferenciaAsync(long transaccionId, long adminId);

    Task RechazarTransferenciaAsync(long transaccionId, string motivo, long adminId);
}