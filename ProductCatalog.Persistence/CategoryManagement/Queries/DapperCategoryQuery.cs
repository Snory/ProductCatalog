using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ProductCatalog.Persistence.Configuration;
using ProductCatalog.Persistence.Utils;
using ProductCatalog.Structures;
using System.Data;

namespace ProductCatalog.Persistence.CategoryManagement.Queries
{
    public class DapperCategoryQuery : ICategoryQuery
    {
        private IDbConnection db;
        private const int _MAXPAGESIZE = 200;


        public DapperCategoryQuery(IOptions<ConnectionStringConfig> connectionStringConfig)
        {
            db = new SqlConnection(connectionStringConfig.Value.DefaultConnection);
        }

        public async Task<(IEnumerable<CategoryView>, PaginationMetadata)> GetAll(int pageNumber = 1, int pageSize = 10)
        {

            if (pageSize > _MAXPAGESIZE)
            {
                pageSize = _MAXPAGESIZE;
            }

            var offset = pageSize * (pageNumber - 1);

            var sql = @"
                SELECT
                    C.Category_ID AS CategoryId,
                    C.Name AS CategoryName
                FROM
                    categoriesmanagement.Categories AS C
                ORDER BY
                    C.Category_ID
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY
            ";

            var categories = await db.QueryAsync<CategoryView>(sql, new { Offset = offset, PageSize = pageSize });
            var paginationMetadata = new PaginationMetadata(categories.ToList().Count, pageSize, pageNumber);

            return (categories, paginationMetadata);
        }

        public async Task<CategoryView?> GetByCategoryId(int id)
        {
            var sql = @"
                SELECT
                    C.Category_ID AS CategoryId,
                    C.Name AS CategoryName
                FROM
                    categoriesmanagement.Categories AS C
                WHERE
                    C.Category_Id = @Id
            ";

            var category = await db.QueryAsync<CategoryView>(sql, new { Id = id });

            return category.FirstOrDefault();
        }
    }
}
