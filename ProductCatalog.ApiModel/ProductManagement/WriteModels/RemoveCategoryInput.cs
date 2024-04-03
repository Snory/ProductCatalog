using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ApiModel.ProductManagement.WriteModels
{
    public class RemoveCategoryInput
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
