using Crud_with_Dapper_And_Serilog.Data;
using Crud_with_Dapper_And_Serilog.Models;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace Crud_with_Dapper_And_Serilog.Repository
{
    public class CustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly DapperContext _context;
        public CustomerRepository(ILogger<CustomerRepository> logger, DapperContext connection)
        {
            _logger = logger;
            _context = connection;
        }


        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            try
            {
                var query = "select * from Customers";
                using var db = _context.CreateConnection();
                return await db.QueryAsync<Customers>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return Enumerable.Empty<Customers>();
            }
        }
        public async Task<Customers> GetCustomersAsync(int CustomerId)
        {
            try
            {
                var query = $"select * from Customers c where c.CustomerId={CustomerId}";
                using var db = _context.CreateConnection();
                return await db.QueryFirstAsync<Customers>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return new Customers { CustomerId = CustomerId };
            }
        }

        public async Task<int> AddAsync(Customers customers)
        {
            try
            {
                var query = "INSERT INTO [dbo].[Customers] ([CustomerId] ,[CustomerName] ,[City] ,[Country]) VALUES (@CustomerId,@CustomerName,@City,@Country)";
                using var db = _context.CreateConnection();
                return await db.ExecuteAsync(query, customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
        public async Task<int> UpdateAsync(int CustomerId, Customers customers)
        {
            try
            {
                var db = _context.CreateConnection();
                var query = "UPDATE [dbo].[Customers] SET [CustomerName] = @CustomerName,[City] = @City,[Country] = @Country WHERE [CustomerId]=" + CustomerId + "";
                var affectedRows = await db.ExecuteAsync(query, new
                {
                    CustomerName = customers.CustomerName,
                    City = customers.City,
                    Country = customers.Country,
                });
                return affectedRows;
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
    }
}
