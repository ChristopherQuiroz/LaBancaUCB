using System;
using System.Data;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Usuario> UsuarioRepository { get; }
        IBaseRepository<Cuenta> CuentaRepository { get; }
        ITransaccionRepository TransaccionRepository { get; }

        IBaseRepository<Beneficiario> BeneficiarioRepository { get; }
        IBaseRepository<Sesione> SesioneRepository { get; }

        IBaseRepository<Seguro> SeguroRepository { get; }
        IBaseRepository<Tarjeta> TarjetaRepository { get; }
        IBaseRepository<Prestamo> PrestamoRepository { get; }
        IBaseRepository<Solicitud> SolicitudRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();

        IDbConnection GetConnection();
        IDbTransaction? GetTransaction();

        void SaveChanges();
        Task SaveChangesAsync();
    }
}