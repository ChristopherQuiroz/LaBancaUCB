using LaBancaUCB.Core.CustomEntities;
using LaBancaUCB.Core.DTOs;
using LaBancaUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Services.Interfaces;

public interface ICuentaService
{
    Task<PagedList<Cuenta>> GetCuentasByUsuarioIdAsync(long idUsuario, PaginationFilter filters);
}