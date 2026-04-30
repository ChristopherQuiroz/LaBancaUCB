using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LaBancaUCB.Core.Enums;

namespace LaBancaUCB.Core.Interfaces;

public interface IDapperContext
{
    DatabaseProvider DatabaseProvider { get; }

    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text);

    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text);

    Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text);

    void SetAmbientConnection(IDbConnection connection, IDbTransaction? transaction = null);
    void ClearAmbientConnection();
}