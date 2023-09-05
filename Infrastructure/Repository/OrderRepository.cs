
using Application.Interfaces;
using Infrastructure.Repository;
using Interview.Backend.Entities;
using Interview.Backend.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Validations;

namespace Dapper.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string? _dbConnectionString;
        private readonly IOrderStepRepository _orderStepRepository;
        private readonly ILoggerService _loggerService;
        public OrderRepository(IConfiguration configuration,
            IOrderStepRepository orderStepRepository,
            ILoggerService loggerService
            )
        {
            _dbConnectionString = configuration.GetConnectionString("DefaultConnection");
            _orderStepRepository = orderStepRepository;
            _loggerService = loggerService;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync()
        {
            var sql = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Order>(sql);
                return result.ToList();
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            string sql = @"
                        SELECT 
	                        o.IdOrder, o.OrderStepId, o.PaymentMethodId, o.CustomerId, o.DateCreate, o.DateEdit,
	                        os.IdOrderStep, os.Description,
	                        pm.IdPaymentMethod, pm.Description,
	                        c.IdCustomer, c.FirstName, c.LastName, c.CompanyVat, c.CompanyName,
	                        p.IdProduct, p.Title, p.Description, p.Price
                        FROM dbo.Orders o
	                        INNER JOIN dbo.OrderSteps os
		                        ON os.IdOrderStep = o.OrderStepId
	                        INNER JOIN dbo.PaymentMethods pm
		                        ON pm.IdPaymentMethod = o.PaymentMethodId
	                        INNER JOIN dbo.Customers c
		                        ON c.IdCustomer = o.CustomerId
	                        INNER JOIN dbo.OrderProducts op
		                        ON op.OrderId = o.IdOrder 
	                        INNER JOIN Products p 
                                ON op.ProductId = p.IdProduct
                        WHERE o.IdOrder  = @Id";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                try
                {
                    connection.Open();

                    var orders = await connection.QueryAsync<Order, OrderStep, PaymentMethod, Customer, Product, Order>(
                                    sql,
                                    (order, orderStep, paymentMethod, customer, product) =>
                                    {
                                        order.OrderStep = orderStep;
                                        order.PaymentMethod = paymentMethod;
                                        order.Customer = customer;
                                        order.Products.Add(product);
                                        return order;
                                    },
                                    new { Id = id },
                                    splitOn: "IdOrder,IdOrderStep,IdPaymentMethod,IdCustomer, IdProduct"
                                    );

                    var result = orders.GroupBy(p => p.IdOrder).Select(g =>
                    {
                        var groupedPost = g.First();
                        groupedPost.Products = g.Select(p => p.Products.Single()).ToList();
                        return groupedPost;
                    }).Distinct().FirstOrDefault();

                    if (result == null)
                        _loggerService.Log($"the order with {id} was not found", LogLevel.Warning);

                    _loggerService.Log($"order retrieved with id {result.IdOrder} ", LogLevel.Trace);
                    return result;
                }
                catch (Exception ex)
                {

                    _loggerService.Log(ex.InnerException.Message, LogLevel.Warning);
                    throw;
                }

            }
        }


        public async Task<int> AddAsync(Order entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        //Validator Step Order
                        OrdersValidator validator = new OrdersValidator(_orderStepRepository);
                        var validatorResult = validator.Validate(entity);
                        if (!validatorResult.IsValid)
                            return default;

                        var sqlInsertOrder = @"
                                INSERT INTO Orders (OrderStepId, PaymentMethodId, CustomerId, DateCreate)
                                OUTPUT INSERTED.IdOrder
                                VALUES (@OrderStepId, @PaymentMethodId, @CustomerId, GETDATE())";

                        var sqlInsertProducts = @"
                                INSERT INTO Products (Title, Description, Price, IsVisible, IsMultiPurchase) 
                                OUTPUT INSERTED.IdProduct
                                VALUES (@Title, @Description, @Price, @IsVisible, @IsMultiPurchase)";

                        string sqlInsertRelations = @"
                                INSERT INTO OrderProducts (OrderId, ProductId) 
                                VALUES (@OrderId, @ProductId)";

                        var result = await connection.ExecuteScalarAsync<int>(sqlInsertOrder, entity, transaction);

                        foreach (var p in entity.Products)
                        {
                            var productId = await connection.ExecuteScalarAsync<int>(sqlInsertProducts, p, transaction);

                            await connection.ExecuteAsync(sqlInsertRelations, new { OrderId = result, ProductId = productId }, transaction);
                        }
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
                        var sql = "DELETE FROM Orders WHERE IdOrder = @Id";
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

        public async Task<int> UpdateAsync(Order entity)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"UPDATE Orders
                                    SET 
                                        OrderStepId = @OrderStepId, 
                                        PaymentMethodId = @PaymentMethodId, 
                                        CustomerId = @CustomerId, 
                                        DateEdit = GETDATE()  
                                    WHERE IdOrder = @Id";
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
