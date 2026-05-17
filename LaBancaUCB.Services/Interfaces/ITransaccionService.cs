using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.CustomEntities;

namespace LaBancaUCB.Services.Interfaces;

public interface ITransaccionService
{
    Task<PagedList<Transaccion>> GetHistorialByUsuarioIdAsync(long idUsuario, TransaccionQueryFilter? filters = null);

    Task<PagedList<Transaccion>> ListarTransferenciasPorEstadoAsync(string? estado, PaginationFilter filters);

    Task<Transaccion> CrearTransferenciaExteriorAsync(TransferenciaExteriorDto dto, long usuarioId);
    Task AprobarTransferenciaAsync(long transaccionId, long adminId);
    Task RechazarTransferenciaAsync(long transaccionId, string motivo, long adminId);
}