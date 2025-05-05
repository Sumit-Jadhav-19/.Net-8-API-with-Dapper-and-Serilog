using Crud_with_Dapper_And_Serilog.Data;
using Crud_with_Dapper_And_Serilog.Models;
using Dapper;
using System.Collections;

namespace Crud_with_Dapper_And_Serilog.Repository
{
    public class CategoriesRepository
    {
        private readonly ILogger<CategoriesRepository> _logger;
        private readonly DapperContext _context;
        public CategoriesRepository(ILogger<CategoriesRepository> logger, DapperContext context)
        {
            this._logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<Categories>> CategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var query = "select * from Categories";
                using var db = _context.CreateConnection();
                return await db.QueryAsync<Categories>(query, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return new List<Categories>();
            }
        }
        public async Task<Categories> CategoryByIdAsync(int CategoryId,CancellationToken cancellationToken)
        {
            try
            {
                var query = "select * from Categories c where c.CategoryId=" + CategoryId;
                using var db = _context.CreateConnection();
                return await db.QueryFirstAsync<Categories>(query,cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return new Categories();
            }
        }

        public async Task<int> AddCategoryAsync(Categories categories)
        {
            try
            {
                var query = "INSERT INTO [dbo].[Categories] ([CategoryId] ,[CategoryName]) VALUES (@CategoryId,@CategoryName)";
                using var db = _context.CreateConnection();
                return await db.ExecuteAsync(query, categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
        public async Task<int> UpdateCategoryAsync(int CategoryId, string CategoryName)
        {
            try
            {
                var query = "UPDATE [dbo].[Categories] SET [CategoryName] =@CategoryName WHERE [CategoryId]=" + CategoryId;
                using var db = _context.CreateConnection();
                return await db.ExecuteAsync(query, new
                {
                    CategoryName = CategoryName,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
        public async Task<int> DeleteCategoryAsync(int CategoryId)
        {
            try
            {
                var query = "DELETE FROM [dbo].[Categories] WHERE CategoryId=" + CategoryId;
                using var db = _context.CreateConnection();
                return await db.ExecuteAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
    }
}
