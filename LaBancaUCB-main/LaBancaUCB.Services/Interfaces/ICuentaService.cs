using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Services.Interfaces;

public interface ICuentaService
{
    Task<IEnumerable<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario);
}