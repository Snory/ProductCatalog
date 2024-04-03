using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ApiModel.CategoryManagement.WriteModel
{
    public class CreateCategoryInput
    {
        [Required]
        public required string CategoryName { get; set;}
    }
}
