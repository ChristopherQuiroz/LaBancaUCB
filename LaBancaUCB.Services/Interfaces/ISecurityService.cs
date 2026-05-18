using LaBancaUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaBancaUCB.Services.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
    }
}
