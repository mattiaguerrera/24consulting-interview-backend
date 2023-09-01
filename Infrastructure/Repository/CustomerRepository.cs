
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Dapper.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string? _dbConnectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            this._dbConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            var sql = "SELECT * FROM Customers";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Customer>(sql);
                return result.ToList();
            }
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Customers WHERE Id = @Id";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> AddAsync(Customer entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"INSERT INTO Customers (FirstName, LastName, CompanyVat, CompanyName) 
                        VALUES (@FirstName, @LastName, @CompanyVat, @CompanyName)";
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
                        var sql = "DELETE FROM Customers WHERE Id = @Id";
                        var result = await connection.ExecuteAsync(sql, new { Id = id });
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

        public async Task<int> UpdateAsync(Customer entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"UPDATE Customers
                                  SET FirstName = @FirstName, LastName = @LastName, CompanyVat = @CompanyVat, CompanyName = @CompanyName
                                  WHERE Id = @Id";
                        var result = await connection.ExecuteAsync(sql, entity);
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
