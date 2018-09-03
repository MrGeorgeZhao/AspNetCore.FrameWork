using System;
using System.Linq;
using NetCoreFramework.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreFramework.Infra.Data.Repository
{
    public class BaseRepository
    {
        protected readonly string _connect;

        public BaseRepository(string conect)
        {
            this._connect = conect;
        }

        protected async Task<T> ExecuteWithCondition<T>(Func<IDbConnection, T> execute)
        {
            try
            {
                using (var connection = new SqlConnection(_connect))
                {
                    await connection.OpenAsync();
                    return execute(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
        }

        protected async Task<T> ExecuteWithConditionAsync<T>(Func<IDbConnection, Task<T>> execute)
        {
            try
            {
                using (var connection = new SqlConnection(_connect))
                {
                    await connection.OpenAsync();
                    return await execute(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
        }


        protected T ExecuteWithConditionSynchro<T>(Func<IDbConnection, T> execute)
        {
            try
            {
                using (var connection = new SqlConnection(_connect))
                {
                    connection.Open();
                    return execute(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
        }

        protected async Task<int> ExecuteWithTransactionAsync(IDbConnection conn, string sql, object param)
        {
            using (IDbTransaction transaction = conn.BeginTransaction())
            {
                int rowCount = 0;
                try
                {
                    rowCount = await conn.ExecuteAsync(sql, param, transaction);
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
                return rowCount;
            }
        }

    }
}
