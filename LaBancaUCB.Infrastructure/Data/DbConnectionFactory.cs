using System;
using System.Data;
using LaBancaUCB.Core.Enums;
using LaBancaUCB.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector; 

namespace LaBancaUCB.Infrastructure.Data;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _mysqlConnection;
    public DatabaseProvider Provider { get; }

    public DbConnectionFactory(IConfiguration config)
    {
        _mysqlConnection = config.GetConnectionString("DefaultConnection") ?? string.Empty;

        var providerStr = config.GetSection("DatabaseProvider").Value;

        if (string.Equals(providerStr, "MySQL", StringComparison.OrdinalIgnoreCase))
        {
            Provider = DatabaseProvider.MySQL;
        }
        else
        {
            Provider = DatabaseProvider.SQLServer;
        }
    }

    public IDbConnection CreateConnection()
    {
        switch (Provider)
        {
            case DatabaseProvider.MySQL:
                return new MySqlConnection(_mysqlConnection);
            default:
                return new MySqlConnection(_mysqlConnection);
        }
    }
}