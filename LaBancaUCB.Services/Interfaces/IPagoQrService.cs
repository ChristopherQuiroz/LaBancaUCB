using LaBancaUCB.Core.DTOs;

namespace LaBancaUCB.Services.Interfaces;

public interface IPagoQrService
{
    Task ProcesarPagoAsync(ProcesarPagoQrDto dto, long idUsuarioAutenticado);
    Task<string> GenerarQrAsync(GenerarQrDto dto, long idUsuarioAutenticado);
}
