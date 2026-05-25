using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Exceptions;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Services.Interfaces;

namespace LaBancaUCB.Services.Services;

public class PagoQrService : IPagoQrService
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoQrService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ProcesarPagoAsync(ProcesarPagoQrDto dto, long idUsuarioAutenticado)
    {
        var cuentaOrigen = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.IdCuentaOrigen);
        if (cuentaOrigen == null || cuentaOrigen.IdUsuario != idUsuarioAutenticado)
            throw new BusinessException("La cuenta de origen no existe o no te pertenece.", HttpStatusCode.Forbidden);

        if (cuentaOrigen.Estado != "activada")
            throw new BusinessException("Tu cuenta no está activa para realizar pagos.", HttpStatusCode.BadRequest);

        var todosLosQr = await _unitOfWork.CodigoQrRepository.GetAllAsync();
        var qr = todosLosQr.FirstOrDefault(q => q.CodigoHash == dto.CodigoHash);

        if (qr == null)
            throw new BusinessException("El código QR no es válido o no existe.", HttpStatusCode.NotFound);

        if (!qr.Activo || qr.ExpiraEn < DateTime.UtcNow)
            throw new BusinessException("El código QR ha expirado o está inactivo.", HttpStatusCode.BadRequest);

        decimal montoAPagar;
        if (qr.EsMontoVariable)
        {
            if (!dto.MontoIngresado.HasValue || dto.MontoIngresado <= 0)
                throw new BusinessException("El QR es de monto variable. Debes ingresar un monto mayor a 0.", HttpStatusCode.BadRequest);
            montoAPagar = dto.MontoIngresado.Value;
        }
        else
        {
            montoAPagar = qr.MontoFijo ?? 0;
        }

        if (cuentaOrigen.Saldo < montoAPagar)
            throw new BusinessException("Saldo insuficiente para realizar el pago QR.", HttpStatusCode.BadRequest);

        var cuentaDestino = await _unitOfWork.CuentaRepository.GetByIdAsync(qr.IdCuentaReceptora);
        if (cuentaDestino == null || cuentaDestino.Estado != "activada")
            throw new BusinessException("La cuenta receptora del QR no está disponible.", HttpStatusCode.BadRequest);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            cuentaOrigen.Saldo -= montoAPagar;
            cuentaDestino.Saldo += montoAPagar;

            _unitOfWork.CuentaRepository.Update(cuentaOrigen);
            _unitOfWork.CuentaRepository.Update(cuentaDestino);

            var transaccion = new Transaccion
            {
                IdCuentaOrigen = cuentaOrigen.IdCuenta,
                IdCuentaDestino = cuentaDestino.NumeroCuenta,
                NombreDestino = qr.Descripcion ?? "Pago por QR",
                Tipo = "qr",
                Monto = montoAPagar,
                Moneda = cuentaOrigen.Moneda,
                TipoCambio = 1m, 
                Glosa = "Pago mediante escaneo QR",
                Estado = "completada",
                ReferenciaQr = qr.CodigoHash,
                FechaHora = DateTime.UtcNow
            };

            await _unitOfWork.TransaccionRepository.AddAsync(transaccion);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw new BusinessException("Error interno al procesar el pago. Se revirtieron los cambios.", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<string> GenerarQrAsync(GenerarQrDto dto, long idUsuarioAutenticado)
    {
        var cuentaReceptora = await _unitOfWork.CuentaRepository.GetByIdAsync(dto.IdCuentaReceptora);
        if (cuentaReceptora == null || cuentaReceptora.IdUsuario != idUsuarioAutenticado)
            throw new BusinessException("La cuenta para recibir el pago no existe o no te pertenece.", HttpStatusCode.Forbidden);

        if (cuentaReceptora.Estado != "activada")
            throw new BusinessException("La cuenta debe estar activa para generar un QR de cobro.", HttpStatusCode.BadRequest);

        // Generar un hash único simulando la encriptación de un QR real
        string hashUnico = "QR-" + Guid.NewGuid().ToString().ToUpper().Substring(0, 8) + "-" + DateTime.UtcNow.Ticks;

        var nuevoQr = new CodigoQr
        {
            IdCuentaReceptora = dto.IdCuentaReceptora,
            CodigoHash = hashUnico,
            MontoFijo = dto.MontoFijo,
            EsMontoVariable = dto.EsMontoVariable,
            Descripcion = dto.Descripcion,
            Activo = true,
            ExpiraEn = DateTime.UtcNow.AddDays(1), // Le damos 1 día de vigencia al código
            CreadoEn = DateTime.UtcNow
        };

        await _unitOfWork.CodigoQrRepository.AddAsync(nuevoQr);
        await _unitOfWork.SaveChangesAsync();

        return hashUnico;
    }
}