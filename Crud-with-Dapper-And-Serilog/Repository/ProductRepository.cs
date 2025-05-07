using Crud_with_Dapper_And_Serilog.Data;
using Crud_with_Dapper_And_Serilog.Models;
using Dapper;

namespace Crud_with_Dapper_And_Serilog.Repository
{
    public class ProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly DapperContext _contex;
        public ProductRepository(ILogger<ProductRepository> logger, DapperContext contex)
        {
            this._logger = logger;
            _contex = contex;
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            try
            {
                var query = "select p.ProductId,p.ProductName,p.CategoryId,c.CategoryName,p.Price from Products p inner join Categories c on p.CategoryId=c.CategoryId";
                var db = _contex.CreateConnection();
                return await db.QueryAsync<Products>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return Enumerable.Empty<Products>();
            }
        }
        public async Task<Products> GetProductAsync(int ProductId)
        {
            try
            {
                var query = "select p.ProductId,p.ProductName,p.CategoryId,c.CategoryName,p.Price from Products p inner join Categories c on p.CategoryId=c.CategoryId where p.ProductId=@ProductId";
                var parameter = new { ProductId = ProductId };
                var db = _contex.CreateConnection();
                return await db.QueryFirstOrDefaultAsync<Products>(query, parameter);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return new Products();
            }
        }
        public async Task<int> AddProductAsync(Products products)
        {
            try
            {
                var db = _contex.CreateConnection();
                var query = "select ISNULL(Max(p.ProductId),0)+1 ProductId from Products p";
                string MaxId = Convert.ToString(await db.ExecuteScalarAsync(query));
                if (!string.IsNullOrEmpty(MaxId)) products.ProductId = Convert.ToInt32(MaxId);
                query = "INSERT INTO [dbo].[Products] ([ProductId] ,[ProductName] ,[CategoryId] ,[Price]) VALUES (@ProductId,@ProductName,@CategoryId,@Price)";
                return await db.ExecuteAsync(query, products);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
        public async Task<int> UpdateProductAsync(int ProdcutId, Products products)
        {
            try
            {
                var query = "UPDATE [dbo].[Products] SET [ProductName] = @ProductName,[CategoryId] = @CategoryId,[Price] = @Price WHERE ProductId=@ProductId";
                var db = _contex.CreateConnection();
                return await db.ExecuteAsync(query, new
                {
                    ProductName = products.ProductName,
                    CategoryId = products.CategoryId,
                    Price = products.Price
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }

        public async Task<int> DeleteProductAsync(int ProductId)
        {
            try
            {
                var query = "DELETE FROM [dbo].[Products] WHERE ProductId=@ProductId";
                var db = _contex.CreateConnection();
                return await db.ExecuteAsync(query, new { ProductId = ProductId });
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, ex);
                return 0;
            }
        }
    }
}
