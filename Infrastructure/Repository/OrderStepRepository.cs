
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repository
{
    public class OrderStepRepository : IOrderStepRepository
    {
        private readonly string? _dbConnectionString;
        public OrderStepRepository(IConfiguration configuration)
        {
            this._dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IReadOnlyList<OrderStep>> GetAllAsync()
        {
            var sql = "SELECT * FROM OrderSteps";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<OrderStep>(sql);
                return result.ToList();
            }
        }

        public async Task<OrderStep> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM OrderSteps WHERE IdOrderStep = @IdOrderStep";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<OrderStep>(sql, new { IdOrderStep = id });
                return result;
            }
        }

        public async Task<int> AddAsync(OrderStep entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT INTO OrderSteps (Description) VALUES (@Description)";
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
                        var sql = "DELETE FROM OrderSteps WHERE IdOrderStep = @IdOrderStep";
                        var result = await connection.ExecuteAsync(sql, new { IdOrderStep = id }, transaction);
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

        public async Task<int> UpdateAsync(OrderStep entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "UPDATE OrderSteps SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE IdOrderStep = @IdOrderStep";
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

        public async Task<int> InsertDefaultValues(List<OrderStep> list)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT OrderSteps (IdOrderStep, Description) 
                                    SELECT @IdOrderStep, @Description
                                    WHERE NOT EXISTS (
                                        SELECT TOP 1 1 
                                        FROM OrderSteps
                                        WHERE IdOrderStep = @IdOrderStep AND Description = @Description
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

        public bool CheckOrderStep(int idOrderStepNew, Order order, out IEnumerable<string> messages)
        {
            bool isValid;
            messages = null;

            string filePath = @"C:\Users\mguerrera\OneDrive - TERRANOVA\Documenti\_repos\app\24consulting_interview\24consulting-interview-backend-main\Infrastructure\Sql\CheckOrderStep.sql";
            string sql = File.ReadAllText(filePath);

            var param = new { IdOrderStep = idOrderStepNew, IdOrder = order.IdOrder };
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                using (var reader = connection.QueryMultiple(sql, param))
                {
                    isValid = reader.ReadSingleOrDefault<bool>();
                    if (!isValid) messages = reader.Read<string>();
                }
            }

            return isValid;
        }

    }
}
