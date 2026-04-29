using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;

namespace LaBancaUCB.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LaBancaUCBContext _context;

        private IBaseRepository<Usuario>? _usuarioRepository;
        private IBaseRepository<Cuenta>? _cuentaRepository;
        private ITransaccionRepository? _transaccionRepository;
        private IBaseRepository<Beneficiario>? _beneficiarioRepository;
        private IBaseRepository<Sesione>? _sesioneRepository;
        private IBaseRepository<Seguro>? _seguroRepository;
        private IBaseRepository<Solicitud>? _solicitudRepository;
        private IBaseRepository<Prestamo>? _prestamoRepository;
        private IBaseRepository<Administrador>? _administradorRepository;

        public UnitOfWork(LaBancaUCBContext context)
        {
            _context = context;
        }

        public IBaseRepository<Usuario> UsuarioRepository => _usuarioRepository ??= new BaseRepository<Usuario>(_context);
        public IBaseRepository<Cuenta> CuentaRepository => _cuentaRepository ??= new BaseRepository<Cuenta>(_context);
        public ITransaccionRepository TransaccionRepository => _transaccionRepository ??= new TransaccionRepository(_context);
        public IBaseRepository<Beneficiario> BeneficiarioRepository => _beneficiarioRepository ??= new BaseRepository<Beneficiario>(_context);
        public IBaseRepository<Sesione> SesioneRepository => _sesioneRepository ??= new BaseRepository<Sesione>(_context);
        public IBaseRepository<Seguro> SeguroRepository => _seguroRepository ??= new BaseRepository<Seguro>(_context);
        public IBaseRepository<Solicitud> SolicitudRepository => _solicitudRepository ??= new BaseRepository<Solicitud>(_context);
        public IBaseRepository<Prestamo> PrestamoRepository => _prestamoRepository ??= new BaseRepository<Prestamo>(_context);
        public IBaseRepository<Administrador> AdministradorRepository => _administradorRepository ??= new BaseRepository<Administrador>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}