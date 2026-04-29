using System;
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
        IBaseRepository<Solicitud> SolicitudRepository { get; }
        IBaseRepository<Prestamo> PrestamoRepository { get; }
        IBaseRepository<Administrador> AdministradorRepository { get; }
        IBaseRepository<Seguro> SeguroRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}