using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LaBancaUCB.Core.Enums;
using LaBancaUCB.Core.Interfaces;

namespace LaBancaUCB.Infrastructure.Data;

public class DapperContext : IDapperContext
{
    private readonly IDbConnectionFactory _connectionFactory;
    private IDbConnection? _ambientConnection;
    private IDbTransaction? _ambientTransaction;

    public DatabaseProvider DatabaseProvider => _connectionFactory.Provider;

    public DapperContext(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void SetAmbientConnection(IDbConnection connection, IDbTransaction? transaction = null)
    {
        _ambientConnection = connection;
        _ambientTransaction = transaction;
    }

    public void ClearAmbientConnection()
    {
        _ambientConnection = null;
        _ambientTransaction = null;
    }

    private async Task<(IDbConnection con, bool isOwned)> GetOpenConnectionAsync()
    {
        if (_ambientConnection != null)
        {
            if (_ambientConnection.State == ConnectionState.Closed) _ambientConnection.Open();
            return (_ambientConnection, false);
        }
        var con = _connectionFactory.CreateConnection();
        con.Open();
        return (con, true);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
    {
        var (con, isOwned) = await GetOpenConnectionAsync();
        try
        {
            return await con.QueryAsync<T>(new CommandDefinition(sql, param, _ambientTransaction, commandType: commandType));
        }
        finally
        {
            if (isOwned) { con.Close(); con.Dispose(); }
        }
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text)
    {
        var (con, isOwned) = await GetOpenConnectionAsync();
        try
        {
            return await con.QuerySingleOrDefaultAsync<T>(new CommandDefinition(sql, param, _ambientTransaction, commandType: commandType));
        }
        finally
        {
            if (isOwned) { con.Close(); con.Dispose(); }
        }
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text)
    {
        var (con, isOwned) = await GetOpenConnectionAsync();
        try
        {
            return await con.ExecuteAsync(new CommandDefinition(sql, param, _ambientTransaction, commandType: commandType));
        }
        finally
        {
            if (isOwned) { con.Close(); con.Dispose(); }
        }
    }
}