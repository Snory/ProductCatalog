using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ProductCatalog.Persistence.Configuration;
using ProductCatalog.Persistence.Utils;
using ProductCatalog.Structures;
using System.Data;

namespace ProductCatalog.Persistence.ProductManagement.Repositories
{
    public class DapperProductQuery : IProductQuery
    {
        private IDbConnection db;
        private const int _MAXPAGESIZE = 200;

        public DapperProductQuery(IOptions<ConnectionStringConfig> connectionStringConfig)
        {
            this.db = new SqlConnection(connectionStringConfig.Value.DefaultConnection);
        }

        public async Task<(IEnumerable<ProductView>, PaginationMetadata)> GetAll(string? categoryFilter = null, int pageNumber = 1, int pageSize = 10)
        {

            if (pageSize > _MAXPAGESIZE)
            {
                pageSize = _MAXPAGESIZE;
            }

            var offset = pageSize * (pageNumber - 1);

            var sql = @"
                SELECT
                    VPC.Product_ID AS ProductId,
                    VPC.Ean,
                    VPC.productDescription,
                    VPC.CurrencyCode,
                    VPC.Price,
                    VPC.LastValueChange,
                    VPC.Quantity,
                    VPC.Category_ID AS CategoryId,
                    VPC.CategoryName
                FROM
                    productsmanagement.v_ProductCategories AS VPC
                WHERE
                    VPC.Product_ID IN (
                        SELECT P.Product_ID
                        FROM
                            productsmanagement.Products AS P 
                        ORDER BY
                            P.Product_ID
                        OFFSET @Offset ROWS
                        FETCH NEXT @PageSize ROWS ONLY
                    )
            ";

            var filterQuery = @"
                AND EXISTS (
                    SELECT 1 
                    FROM
                        productsmanagement.ProductCategories AS PC
                        JOIN categoriesmanagement.Categories AS C ON C.Category_ID = PC.Category_ID
                    WHERE
                        PC.Product_ID = VPC.Product_ID
                        AND C.Name IN @CategoryFilter
                )             
            ";

            string[]? categoryArray = null;

            if (categoryFilter is not null)
            {
                categoryArray = categoryFilter.Split(',');
                
                sql = sql + filterQuery;
            }

            var productDict = new Dictionary<int, ProductView>();

            var products = await db.QueryAsync<ProductView, CategoryView, ProductView>(sql, (product, category) =>
            {
                if(!productDict.TryGetValue(product.ProductId, out var currentProduct))
                {
                    currentProduct = product;
                    productDict.Add(currentProduct.ProductId, product);
                }
                currentProduct.Categories.Add(category);
                return currentProduct;
            }, new { Offset = offset, PageSize = pageSize, CategoryFilter = categoryArray ?? null }, splitOn: "CategoryId");

            var distinctProducts = products.Distinct().ToList();
            var paginationMetadata = new PaginationMetadata(distinctProducts.Count, pageSize, pageNumber);

            return (distinctProducts, paginationMetadata);
        }

        public async Task<ProductView?> GetProductById(int id)
        {
            var sql = @"
                SELECT
                    PC.Product_ID AS ProductId,
                    PC.Ean,
                    PC.productDescription,
                    PC.CurrencyCode,
                    PC.Price,
                    PC.LastValueChange,
                    PC.Quantity,
                    PC.Category_ID AS CategoryId,
                    PC.CategoryName
                FROM
                    productsmanagement.v_ProductCategories AS PC
                WHERE
                    PC.Product_ID = @Id
            ";

            var productDict = new Dictionary<int, ProductView>();

            var products = await db.QueryAsync<ProductView, CategoryView, ProductView>(sql, (product, category) =>
            {
                if (!productDict.TryGetValue(product.ProductId, out var currentProduct))
                {
                    currentProduct = product;
                    productDict.Add(currentProduct.ProductId, product);
                }
                currentProduct.Categories.Add(category);
                return currentProduct;
            }, new { Id = id }, splitOn: "CategoryId");

            var distinctProducts = products.Distinct().FirstOrDefault();
 
            return distinctProducts;
        }
    }
}
