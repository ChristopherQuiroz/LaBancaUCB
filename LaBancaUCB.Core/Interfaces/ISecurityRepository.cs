using LaBancaUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Core.Interfaces
{
    public interface ISecurityRepository : IBaseRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
