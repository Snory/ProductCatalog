using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class View : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW productsmanagement.v_ProductCategories AS 
                    SELECT
                        P.Product_ID,
                        P.Ean,
                        P.Description AS ProductDescription,
                        P.CurrencyCode,
                        P.Price,
                        P.LastValueChange,
                        P.Quantity,
                        C.Category_ID,
                        C.Name AS CategoryName
                    FROM
                        productsmanagement.Products AS P
                        LEFT JOIN productsmanagement.ProductCategories AS PC ON PC.Product_ID = P.Product_ID
                        LEFT JOIN categoriesmanagement.Categories AS C ON C.Category_ID = PC.Category_ID
                        
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW productsmanagement.ProductCategories");
        }
    }
}
