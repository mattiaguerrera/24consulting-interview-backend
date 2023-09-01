
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string? _dbConnectionString;
        public ProductRepository(IConfiguration configuration)
        {
            this._dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE IdProduct = @IdProduct";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> AddAsync(Product entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT INTO Products (Title, Description, Price, IsVisible, IsMultiPurchase) 
                        VALUES (@Title, @Description, @Price, @IsVisible, @IsMultiPurchase)";
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
                        var sql = "DELETE FROM Products WHERE IdProduct = @IdProduct";
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

        public async Task<int> UpdateAsync(Product entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "UPDATE Products SET Title = @Title, Description = @Description, Price = @Price, IsVisible = @IsVisible, IsMultiPurchase = @IsMultiPurchase  WHERE Id = @Id";
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
    }
}
