using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LaBancaUCB.Core.Enums;

namespace LaBancaUCB.Core.Interfaces;

public interface IDbConnectionFactory
{
    DatabaseProvider Provider { get; }
    IDbConnection CreateConnection();
}
