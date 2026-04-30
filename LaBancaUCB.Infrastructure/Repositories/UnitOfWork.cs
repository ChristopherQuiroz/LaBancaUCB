using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;

namespace LaBancaUCB.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LaBancaUCBContext _context;
        private readonly IDapperContext _dapper;
        private IDbContextTransaction? _efTransaction;

        private IBaseRepository<Usuario>? _usuarioRepository;
        private IBaseRepository<Cuenta>? _cuentaRepository;
        private ITransaccionRepository? _transaccionRepository;
        private IBaseRepository<Beneficiario>? _beneficiarioRepository;
        private IBaseRepository<Sesione>? _sesioneRepository;
        private IBaseRepository<Seguro>? _seguroRepository;
        private IBaseRepository<Tarjeta>? _tarjetaRepository;
        private IBaseRepository<Prestamo>? _prestamoRepository;
        private IBaseRepository<Solicitud>? _solicitudRepository;

        public UnitOfWork(LaBancaUCBContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task BeginTransactionAsync()
        {
            if (_efTransaction == null)
            {
                _efTransaction = await _context.Database.BeginTransactionAsync();
                _dapper.SetAmbientConnection(_context.Database.GetDbConnection(), _efTransaction.GetDbTransaction());
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_efTransaction != null) await _efTransaction.CommitAsync();
            }
            finally
            {
                _dapper.ClearAmbientConnection();
                if (_efTransaction != null)
                {
                    await _efTransaction.DisposeAsync();
                    _efTransaction = null;
                }
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_efTransaction != null) await _efTransaction.RollbackAsync();
            }
            finally
            {
                _dapper.ClearAmbientConnection();
                if (_efTransaction != null)
                {
                    await _efTransaction.DisposeAsync();
                    _efTransaction = null;
                }
            }
        }

        public IDbConnection GetConnection() => _context.Database.GetDbConnection();
        public IDbTransaction? GetTransaction() => _efTransaction?.GetDbTransaction();
        public IBaseRepository<Usuario> UsuarioRepository => _usuarioRepository ??= new BaseRepository<Usuario>(_context);
        public IBaseRepository<Cuenta> CuentaRepository => _cuentaRepository ??= new BaseRepository<Cuenta>(_context);
        public ITransaccionRepository TransaccionRepository => _transaccionRepository ??= new TransaccionRepository(_context);
        public IBaseRepository<Beneficiario> BeneficiarioRepository => _beneficiarioRepository ??= new BaseRepository<Beneficiario>(_context);
        public IBaseRepository<Sesione> SesioneRepository => _sesioneRepository ??= new BaseRepository<Sesione>(_context);
        public IBaseRepository<Seguro> SeguroRepository => _seguroRepository ??= new BaseRepository<Seguro>(_context);
        public IBaseRepository<Tarjeta> TarjetaRepository => _tarjetaRepository ??= new BaseRepository<Tarjeta>(_context);
        public IBaseRepository<Prestamo> PrestamoRepository => _prestamoRepository ??= new BaseRepository<Prestamo>(_context);
        public IBaseRepository<Solicitud> SolicitudRepository => _solicitudRepository ??= new BaseRepository<Solicitud>(_context);

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