
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repository
{
    public class OrderStepFlowRepository : IOrderStepFlowRepository
    {
        private readonly string? _dbConnectionString;
        public OrderStepFlowRepository(IConfiguration configuration)
        {
            this._dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IReadOnlyList<OrderStepFlow>> GetAllAsync()
        {
            var sql = "SELECT * FROM OrderStepFlows";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<OrderStepFlow>(sql);
                return result.ToList();
            }
        }

        public async Task<OrderStepFlow> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM OrderStepFlows WHERE Id = @Id";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<OrderStepFlow>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> AddAsync(OrderStepFlow entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT OrderStepFlows (IdStepPrev, IdStepNext) 
                                    SELECT @IdStepPrev, @IdStepNext
                                    WHERE NOT EXISTS (
                                        SELECT TOP 1 1 
                                        FROM OrderStepFlows
                                        WHERE IdStepPrev = @IdStepPrev AND IdStepNext = @IdStepNext
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
                        var sql = "DELETE FROM OrderStepFlows WHERE Id = @Id";
                        var result = await connection.ExecuteAsync(sql, new { Id = id }, transaction);
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

        public async Task<int> UpdateAsync(OrderStepFlow entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "UPDATE OrderStepFlows SET IdStepPrev = @IdStepPrev, IdStepNext = @IdStepNext WHERE Id = @Id";
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

        public async Task<int> InsertDefaultValues(List<OrderStepFlow> list)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT OrderStepFlows (IdStepPrev, IdStepNext) 
                                    SELECT @IdStepPrev, @IdStepNext
                                    WHERE NOT EXISTS (
                                        SELECT TOP 1 1 
                                        FROM OrderStepFlows
                                        WHERE IdStepPrev = @IdStepPrev AND IdStepNext = @IdStepNext
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
