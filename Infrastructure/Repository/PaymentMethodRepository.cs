
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly string? _dbConnectionString;
        public PaymentMethodRepository(IConfiguration configuration)
        {
            this._dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IReadOnlyList<PaymentMethod>> GetAllAsync()
        {
            var sql = "SELECT * FROM PaymentMethods";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<PaymentMethod>(sql);
                return result.ToList();
            }
        }

        public async Task<PaymentMethod> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM PaymentMethods WHERE IdPaymentMethod = @IdPaymentMethod";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<PaymentMethod>(sql, new { IdPaymentMethod = id });
                return result;
            }
        }

        public async Task<int> AddAsync(PaymentMethod entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT PaymentMethods (Description, PaymentMethodType) 
                                    SELECT @Description, @PaymentMethodType
                                    WHERE NOT EXISTS (
                                        SELECT TOP 1 1 
                                        FROM PaymentMethods
                                        WHERE Description = @Description AND PaymentMethodType = @PaymentMethodType
                                    )";
                        var result = await connection.ExecuteAsync(sql, entity, transaction);
                        transaction.Commit();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        // log exception 
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return default;
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "DELETE FROM PaymentMethods WHERE IdPaymentMethod = @IdPaymentMethod";
                        var result = await connection.ExecuteAsync(sql, new { IdPaymentMethod = id }, transaction);
                        transaction.Commit();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        // log exception 
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return default;
        }

        public async Task<int> UpdateAsync(PaymentMethod entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "UPDATE PaymentMethods SET Description = @Description, PaymentMethodType = @PaymentMethodType WHERE IdPaymentMethod = @IdPaymentMethod";
                        var result = await connection.ExecuteAsync(sql, entity, transaction);
                        transaction.Commit();
                        return result;

                    }
                    catch (Exception ex)
                    {
                        // log exception 
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return default;
        }

        public async Task<int> InsertDefaultValues(List<PaymentMethod> list)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT PaymentMethods (IdPaymentMethod, Description) 
                                    SELECT @IdPaymentMethod, @Description
                                    WHERE NOT EXISTS (
                                        SELECT TOP 1 1 
                                        FROM PaymentMethods
                                        WHERE IdPaymentMethod = @IdPaymentMethod AND Description = @Description
                                    )";

                        await connection.ExecuteAsync(sql, list, transaction);
                        transaction.Commit();
                        return list.Count;
                    }
                    catch (Exception ex)
                    {
                        // log exception 
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return default;
        }

    }
}
